using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;


namespace EcsTraining
{
    [UpdateAfter(typeof(ExternalCommunicatorSystem))]
    public partial class DecideActionSystem : SystemBase
    {
        private readonly HashSet<string> _uniqueBrainNames = new HashSet<string>();
        private AgentAction _jobDataCache = new AgentAction();
        protected override void OnUpdate()
        {
            _uniqueBrainNames.Clear();
            
            // We search for the N brains once, and then we iterate over each one. Doing so we avoid doing string lookups for each agent
            foreach (var (brain, agent) in SystemAPI.Query<RefRO<BrainSimple>, RefRW<AgentEcs>>())
            {
                if(!agent.ValueRO.RequestDecision) continue;
                _uniqueBrainNames.Add(brain.ValueRO.FullyQualifiedBehaviorName.Value);
                agent.ValueRW.RequestDecision = false;
            }

            if (_uniqueBrainNames.Count == 0) return;
            
            CommunicatorManager.DecideAction();
            
            foreach (string brainName in _uniqueBrainNames)
            {
                // Expensive lookup that is performed once per brain, instead of once per agent
                var actionsForThisBrain = CommunicatorManager.GetActionsForBrain(brainName);
                if (actionsForThisBrain == null || actionsForThisBrain.Count == 0) continue;
                
                var nativeActions = new NativeHashMap<int, AgentAction>(
                    actionsForThisBrain.Count,
                    Allocator.TempJob
                );

                foreach (var (agentId, actionBuffer) in actionsForThisBrain)
                {
                    foreach (var val in actionBuffer.ContinuousActions.Array) { _jobDataCache.ContinuousActions.Add(val); }
                    foreach (var val in actionBuffer.DiscreteActions.Array) { _jobDataCache.DiscreteActions.Add(val); }
                    
                    nativeActions.Add(agentId, _jobDataCache);
                    _jobDataCache.ContinuousActions.Clear();
                    _jobDataCache.DiscreteActions.Clear();
                }
                
                var job = new DistributeActionsJob
                {
                    ActionsToDistribute = nativeActions
                };
                
                var brainFilter = new BrainSimple { FullyQualifiedBehaviorName = new FixedString32Bytes(brainName) };
                Dependency = job.ScheduleParallel(GetEntityQuery(typeof(AgentEcs), ComponentType.ReadWrite<AgentAction>(), ComponentType.ReadOnly(brainFilter.GetType())), Dependency);
                
                // Need to call JobHandle.Complete() before we can deallocate the Unity.Collections.NativeHashMap
                Dependency.Complete();
                nativeActions.Dispose();
            }
        }
    }
    
    [BurstCompile]
    public partial struct DistributeActionsJob : IJobEntity
    {
        [ReadOnly]
        public NativeHashMap<int, AgentAction> ActionsToDistribute;
        
        public void Execute(in AgentEcs agent, RefRW<AgentAction> actionComponent)
        {
            if (!ActionsToDistribute.TryGetValue(agent.EpisodeId, out var actionData)) return;
            
            actionComponent.ValueRW.ContinuousActions = actionData.ContinuousActions;
            actionComponent.ValueRW.DiscreteActions = actionData.DiscreteActions;
        }
    }
}
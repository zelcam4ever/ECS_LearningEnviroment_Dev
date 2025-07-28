using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static Unity.Entities.SystemAPI;

using Unity.MLAgents.CommunicatorObjects; // For ObservationProto, AgentInfoProto, etc.


namespace EcsTraining
{
    //RequestDecision() in RemotePolicy
    [UpdateAfter(typeof(ObservationCollectionSystem))]
    public partial class ExternalCommunicatorSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            
            foreach (var (agent, policy, actionsSpec, observations) in
                     Query<RefRW<AgentEcs>, RefRO<BrainSimple>, RefRO<ActionsStructure>,DynamicBuffer<ObservationValue>>()
                         .WithAll<RemotePolicy>())
            {
                if (!agent.ValueRO.RequestDecision) continue;

                // Will try to subscribe the same brain multiple times, but we can assure all agents try at least once 
                if (!agent.ValueRO.Initialized)
                {
                    CommunicatorManager.SubscribeBrain(policy.ValueRO.FullyQualifiedBehaviorName.Value, actionsSpec.ValueRO);
                    agent.ValueRW.Initialized = true;
                }
                
                CommunicatorManager.PutObservation(policy.ValueRO.FullyQualifiedBehaviorName.Value, agent.ValueRO, observations);
                
                agent.ValueRW.Reward = 0f;
                agent.ValueRW.GroupReward = 0f;
                
            }
        }
    }
}
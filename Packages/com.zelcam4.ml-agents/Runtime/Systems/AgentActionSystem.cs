using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Zelcam4.MLAgents;
using Zelcam4.MLAgents.Actuators;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;
using Random = UnityEngine.Random;

namespace Zelcam4.MLAgents
{
    [UpdateAfter(typeof(DecideActionSystem))]
    public partial struct AgentActionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Training>(); 
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var actionJob = new AgentActionJob();
            state.Dependency = actionJob.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    public partial struct AgentActionJob : IJobEntity
    {
        private void Execute(ref AgentEcs agent, in AgentAction action, ref LocalTransform transform)
        {
            if (!agent.RequestAction) return;

            agent.RequestAction = false;
            var actionsTaking = action.DiscreteActions;
            
            
            float3 movement;
            switch (actionsTaking[0])
            {
                case 0: movement = new float3(1, 0, 0); break;
                case 1: movement = new float3(-1, 0, 0); break;
                case 2: movement = new float3(0, 0, 1); break;
                case 3: movement = new float3(0, 0, -1); break;
                default: movement = float3.zero; break;
            }
            transform.Position += movement;

        }
    }
}
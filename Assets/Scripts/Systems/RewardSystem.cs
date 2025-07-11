using Unity.Entities;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(AgentActionSystem))]
    public partial struct RewardSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
    
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, transform) in Query<RefRW<AgentEcs>, RefRO<LocalTransform>>())
            {
                float reward = 0f;
                if (math.distance(transform.ValueRO.Position,
                        GetComponent<LocalTransform>(agent.ValueRO.Target).Position) < 0.5f)
                {
                    reward = 100f;
                }
                else reward -= 0.1f;

                agent.ValueRW.Reward += reward;
                agent.ValueRW.CumulativeReward += reward;
            }
        }
    }
}
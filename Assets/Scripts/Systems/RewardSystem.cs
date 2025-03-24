using Unity.Entities;
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
            foreach (var agent in Query<RefRW<Agent>>())
            {
                agent.ValueRW.Reward += 1;
                agent.ValueRW.CumulativeReward += 1;
            }
        }
    }
}
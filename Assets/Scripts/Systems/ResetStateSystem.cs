using Unity.Entities;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(ExternalPolicySystem))]
    public partial struct ResetStateSystem : ISystem
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
                if (agent.ValueRO.RequestDecision)
                {
                    //agent.ValueRW.Reward = 0;
                    agent.ValueRW.GroupReward = 0;
                    agent.ValueRW.RequestDecision = false;
                }
            }
        }
    }
}
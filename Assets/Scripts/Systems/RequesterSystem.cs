using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    /// <summary>
    /// Equivalent to DecisionRequester. Iterates over all Agents and check whether they request Decision or Actions
    /// </summary>
    [UpdateAfter(typeof(InitializeEnvironmentSystem))]
    public partial struct RequesterSystem: ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
    
        public void OnUpdate(ref SystemState state)
        {
            foreach (var agent in Query<RefRW<AgentEcs>>())
            {
                if (true)
                {
                    agent.ValueRW.RequestDecision = true;
                    agent.ValueRW.RequestAction = true;
                }

                if (true)
                {
                    agent.ValueRW.RequestAction = true;
                }
            }
        }
    }
}
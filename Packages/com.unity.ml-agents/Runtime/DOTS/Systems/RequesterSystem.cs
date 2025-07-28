using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace Zelcam4.MLAgents.DOTS
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
            var academyStepCount = GetSingleton<AcademyTraining>().StepCount;
            foreach (var (agent, decisionRequest) in Query<RefRW<AgentEcs>, RefRO<DecisionRequest>>())
            {
                if (academyStepCount % decisionRequest.ValueRO.DecisionPeriod == decisionRequest.ValueRO.DecisionStep)
                {
                    agent.ValueRW.RequestDecision = true;
                    agent.ValueRW.RequestAction = true;
                    continue;
                }

                if (decisionRequest.ValueRO.TakeActionsBetweenDecisions)
                {
                    agent.ValueRW.RequestAction = true;
                }
            }
        }
    }
}
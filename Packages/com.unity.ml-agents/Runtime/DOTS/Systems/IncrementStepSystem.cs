using Unity.Burst;
using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace MLAgents.DOTS
{
    /// <summary>
    /// Handles the episode steps, substituting the Academy event.
    /// </summary>
    [UpdateAfter(typeof(RequesterSystem))]
    public partial struct IncrementStepSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
        
        
        public void OnUpdate(ref SystemState state)
        {
            GetSingletonRW<AcademyTraining>().ValueRW.StepCount+=1;
            GetSingletonRW<AcademyTraining>().ValueRW.TotalStepCount+=1;
            
            foreach (var agent in Query<RefRW<AgentEcs>>())
            {
                agent.ValueRW.StepCount += 1;
            }
        }
        
    }
}
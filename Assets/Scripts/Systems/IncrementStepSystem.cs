using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
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
            
            foreach (var agent in Query<RefRW<Agent>>())
            {
                agent.ValueRW.StepCount += 1;
            }
        }


        /*[BurstCompile]
        public partial struct IncrementAgentStepJob : IJobEntity
        {
            
        }*/
    }
}
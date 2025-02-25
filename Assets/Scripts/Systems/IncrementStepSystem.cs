using Unity.Burst;
using Unity.Entities;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    /// <summary>
    /// Handles the episode steps, substituting the Academy event.
    /// </summary>
    public partial struct IncrementStepSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var academy = GetSingleton<AcademyTraining>();
            academy.StepCount += 1;
            academy.TotalStepCount += 1;
            
            //Foreach AgentIncrementStep()
            /*foreach (var agent in Query<RefRW<AgentInfo>>())
            {
                agent.ValueRW.StepCount += 1;
            }*/
        }


        /*[BurstCompile]
        public partial struct IncrementAgentStepJob : IJobEntity
        {
            
        }*/
    }
}
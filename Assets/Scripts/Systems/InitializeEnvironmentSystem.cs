using Unity.Burst;
using Unity.Entities;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    public partial struct InitializeEnvironmentSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var academy = GetSingletonRW<AcademyTraining>().ValueRW;

            if (academy.IsInitialized)return;
            academy.IsInitialized = true;
            

        }
        
    }
}
    


using Unity.Entities;
using Unity.Mathematics;

namespace EcsTraining
{
    public struct Observation : IComponentData
    {
        public float3 OwnPosition;
        public float3 TargetPosition;
    }
}
using Unity.Entities;
using Unity.Mathematics;

namespace EcsTraining
{
    public struct Observation : IComponentData
    {
        public float3 OwnPosition;
        public float3 TargetPosition;
    }

    [InternalBufferCapacity(8)]
    public struct ObservationElementData : IBufferElementData
    {
        public float Value;
    }
}
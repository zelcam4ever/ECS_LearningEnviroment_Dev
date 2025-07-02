using Unity.Entities;
using Unity.Mathematics;

namespace EcsTraining
{
    public struct Observation : IComponentData
    {
        public float3 OwnPosition;
        public float3 TargetPosition;
    }

    public struct ObservationValue : IBufferElementData
    {
        public float Value;
        
        // Convenience
        public static implicit operator float(ObservationValue e) { return e.Value; }
        public static implicit operator ObservationValue(float f) { return new ObservationValue { Value = f }; }
    }
    
    public enum ObservationSourceType
    {
        PositionX, PositionY, PositionZ,
        VelocityX, VelocityY, VelocityZ
    }
    
    public struct ObservationSource : IBufferElementData
    {
        public ObservationSourceType SourceType;
    }
}
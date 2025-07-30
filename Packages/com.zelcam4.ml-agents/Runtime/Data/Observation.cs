using Unity.Entities;
using Unity.Mathematics;

namespace Zelcam4.MLAgents
{
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
        PositionXTarget, PositionYTarget, PositionZTarget
    }
    
    public struct ObservationSource : IBufferElementData
    {
        public ObservationSourceType SourceType;
    }
}
using Unity.Entities;
using Unity.Mathematics;

namespace EcsTraining
{
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
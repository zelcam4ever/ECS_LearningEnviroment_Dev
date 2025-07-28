using Unity.Entities;
using Unity.Mathematics;

namespace Zelcam4.MLAgents.DOTS
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
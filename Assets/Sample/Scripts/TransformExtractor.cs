using Unity.Entities;
using Unity.Transforms;
using Zelcam4.MLAgents;

// Define simple, reusable config structs inside this class.
[System.Serializable]
public struct TransformConfig
{
    public TransformSourceId SourceType;
}

// Define clear, readable IDs for the different values within LocalTransform.
public enum TransformSourceId : byte
{
    PositionX = 0,
    PositionY = 1,
    PositionZ = 2,
}
public struct TransformExtractor : IObservationExtractor<LocalTransform>
{
    public float Extract(in LocalTransform component, byte sourceSubId)
    {
        // Cast the byte ID back to an enum for the switch statement
        switch ((TransformSourceId)sourceSubId)
        {
            case TransformSourceId.PositionX: return component.Position.x;
            case TransformSourceId.PositionY: return component.Position.y;
            case TransformSourceId.PositionZ: return component.Position.z;
            default: return 0f;
        }
    }
}

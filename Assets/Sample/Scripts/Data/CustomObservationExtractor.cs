using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Zelcam4.MLAgents;

// Example of customobservation component
public struct CustomObservation : IComponentData
{
    public Entity Target;
    public float3 Position;
}

// Define config structs inside this class
[System.Serializable]
public struct CustomObservationConfig
{
    public CustomObservationSourceId SourceType;
}

// Define IDs for the different values within LocalTargetInfo
public enum CustomObservationSourceId : byte { TargetPositionX = 0, TargetPositionY = 1, TargetPositionZ = 2};

public struct CustomObservationExtractor : IObservationExtractor<CustomObservation>
{
    public float Extract(in CustomObservation component, byte sourceSubId)
    {
        // Cast the byte ID back to an enum for the switch statement
        switch ((CustomObservationSourceId)sourceSubId)
        {
            case CustomObservationSourceId.TargetPositionX: return component.Position.x;
            case CustomObservationSourceId.TargetPositionY: return component.Position.y;
            case CustomObservationSourceId.TargetPositionZ: return component.Position.z;
            default: return 0f;
        }
    }
}

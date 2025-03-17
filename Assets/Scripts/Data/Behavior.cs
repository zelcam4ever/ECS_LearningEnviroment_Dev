using Unity.Entities;

namespace EcsTraining
{
    public struct BehaviorTypeDefault : IComponentData { }
    public struct BehaviorTypeHeuristicOnly : IComponentData { }
    public struct BehaviorTypeInferenceOnly : IComponentData { }
    
    public struct RemotePolicy : IComponentData { }
}
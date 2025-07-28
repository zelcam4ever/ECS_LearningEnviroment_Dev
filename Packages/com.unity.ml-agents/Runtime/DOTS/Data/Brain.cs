using System.Linq;
using Unity.Collections;
using Unity.Entities;

using Unity.MLAgents.Actuators;

namespace Zelcam4.MLAgents.DOTS
{
    public struct BrainSimple: IComponentData
    {
        public FixedString32Bytes FullyQualifiedBehaviorName;
    }
    
    public struct RemotePolicy : IComponentData { }
    public struct InferencePolicy : IComponentData { }
    public struct HeuristicPolicy : IComponentData { }
    
}

using System.Linq;
using Unity.Collections;
using Unity.Entities;

using Unity.MLAgents.Actuators;

namespace EcsTraining
{
    public struct BrainParameter: IComponentData
    {
        public int VectorObservationSize;
        public int StackedVectorObservation;
        //public ActionSpec ActionType;
        public int ActionSize;
        public int TeamId;
    }
    
    public struct BrainSimple: IComponentData
    {
        public int AgentId;
        public FixedString32Bytes FullyQualifiedBehaviorName;
        //public string FullyQualifiedBehaviorName;
        //public ActionSpec ActionType;
        //public ActionBuffers LastActionBuffer;
    }

    
   
}

using System.Linq;
using Unity.Collections;
using Unity.Entities;

namespace EcsTraining
{
    public struct BrainParameter: IComponentData
    {
        public int VectorObservationSize;
        public int StackedVectorObservation;
        public ActionSpec ActionType;
        public int ActionSize;
        public int TeamId;
    }

    
    //WIP: Implement to configure ActionSpace
    public struct ActionSpec : IComponentData
    {
        public int NumContinuousActions;
        public NativeArray<int> BranchSizes;
        
        /// <summary>
        /// The number of branches for discrete actions that an Agent can take.
        /// </summary>
        public int NumDiscreteActions => BranchSizes == null ? 0 : BranchSizes.Length;
        
        /// <summary>
        /// Get the total number of Discrete Actions that can be taken by calculating the Sum
        /// of all the Discrete Action branch sizes.
        /// </summary>
        public int SumOfDiscreteBranchSizes => BranchSizes == null ? 0 : BranchSizes.Sum();
    }
}

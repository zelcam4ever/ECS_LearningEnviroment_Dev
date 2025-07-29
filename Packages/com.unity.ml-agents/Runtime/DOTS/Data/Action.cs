using Unity.Collections;
using Unity.Entities;

namespace Zelcam4.MLAgents.DOTS
{
    /// <summary>
    /// A component that stores the action decided by the policy.
    /// This data changes every decision step.
    /// </summary>
    public struct AgentAction : IComponentData
    {
        public FixedList64Bytes<float> ContinuousActions;

        // This will store the chosen action for EACH discrete branch.
        // Its length will be equal to the number of branches.
        public FixedList64Bytes<int> DiscreteActions; 
    }
}
using Unity.Collections;
using Unity.Entities;

namespace EcsTraining
{
    public struct AgentAction : IComponentData
    {
        public FixedList64Bytes<float> ContinuousActions;
        public FixedList64Bytes<int> DiscreteActions;
    }
}
using Unity.Entities;

namespace EcsTraining
{
    public struct DecisionRequest : IComponentData
    {
        public int DecisionPeriod;
        public int DecisionStep;
        public bool TakeActionsBetweenDecisions;
    }
}
using Unity.Entities;

namespace MLAgents.DOTS
{
    public struct DecisionRequest : IComponentData
    {
        public int DecisionPeriod;
        public int DecisionStep;
        public bool TakeActionsBetweenDecisions;
    }
}
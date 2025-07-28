using Unity.Entities;

namespace Zelcam4.MLAgents.DOTS
{
    public struct DecisionRequest : IComponentData
    {
        public int DecisionPeriod;
        public int DecisionStep;
        public bool TakeActionsBetweenDecisions;
    }
}
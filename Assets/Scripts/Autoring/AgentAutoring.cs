using Unity.Entities;
using UnityEngine;

namespace EcsTraining
{
    public class AgentAutoring: MonoBehaviour
    {
        public float Reward;
        public bool RequestAction;
        public bool RequestDecision;
        public int StepCount;
        public int EpisodeId;
    }
    
    class Baker : Baker<AgentAutoring>
    {
        public override void Bake(AgentAutoring authoring)
        {
            
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new AgentSimple() //TODO: Change proper agentsº
            {
                Reward = authoring.Reward,
                RequestAction = authoring.RequestAction,
                RequestDecision = authoring.RequestDecision,
                StepCount = authoring.StepCount,
                EpisodeId = authoring.EpisodeId
            });
            AddComponent(entity, new Observation() 
            {
                Value = 5
            });
        }

      
    }
}
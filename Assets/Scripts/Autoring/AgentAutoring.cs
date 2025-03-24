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
        public int AgentInfoId;
        
        public Transform SpawnPoint;
        
        private class Baker : Baker<AgentAutoring>
        {
            public override void Bake(AgentAutoring authoring)
            {
            
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Agent() 
                {
                    Reward = authoring.Reward,
                    RequestAction = authoring.RequestAction,
                    RequestDecision = authoring.RequestDecision,
                    StepCount = authoring.StepCount,
                    EpisodeId = authoring.EpisodeId,
                    AgentInfoId = authoring.AgentInfoId,
                    Target = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic),
                });
                AddComponent(entity, new Observation() 
                {
                    //TargetPosition = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic),
                });
                AddComponent(entity, new RemotePolicy());
                AddComponent(entity, new BrainSimple() 
                {
                    FullyQualifiedBehaviorName = "a"
                });
                AddComponent(entity, new Action());
                AddComponent(entity, new AgentReset());
                SetComponentEnabled<AgentReset>(entity, false);
                AddComponent(entity, new OnEpisodeBegin());
                SetComponentEnabled<OnEpisodeBegin>(entity, false);
                
            }
        }
    }

    
}
using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;

namespace EcsTraining
{
    public class AgentAutoring: MonoBehaviour
    {
        public float Reward;
        public bool RequestAction;
        public bool RequestDecision;
        public int StepCount;
        public int MaxStep;
        
        public Transform SpawnPoint;
        public Transform GroundRender;
        
        private class Baker : Baker<AgentAutoring>
        {
            public override void Bake(AgentAutoring authoring)
            {
            
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new AgentEcs() 
                {
                    Reward = authoring.Reward,
                    RequestAction = authoring.RequestAction,
                    RequestDecision = authoring.RequestDecision,
                    StepCount = authoring.StepCount,
                    MaxStep = authoring.MaxStep,
                    EpisodeId = EpisodeIdCounter.GetEpisodeId(),
                    Target = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic),
                    GoundRender = GetEntity(authoring.GroundRender, TransformUsageFlags.None),
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
using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;

namespace EcsTraining
{
    public class AgentAutoring: MonoBehaviour
    {
        public int MaxStep;
        
        public ObservationSourceType[] observationSetup;
        public Transform SpawnPoint;
        public Transform GroundRender;
        
        private class Baker : Baker<AgentAutoring>
        {
            public override void Bake(AgentAutoring authoring)
            {
            
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new AgentEcs() 
                {
                    MaxStep = authoring.MaxStep,
                    EpisodeId = EpisodeIdCounter.GetEpisodeId(),
                    Target = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic),
                    GoundRender = GetEntity(authoring.GroundRender, TransformUsageFlags.None),
                });
                AddComponent(entity, new Observation() 
                {
                    //TargetPosition = GetEntity(authoring.SpawnPoint, TransformUsageFlags.Dynamic),
                });
                
                //Values buffer
                DynamicBuffer<ObservationValue> observations = AddBuffer<ObservationValue>(entity);
                observations.ResizeUninitialized(authoring.observationSetup.Length);
                //Config buffer
                DynamicBuffer<ObservationSource> sources = AddBuffer<ObservationSource>(entity);
                sources.Capacity = authoring.observationSetup.Length;
                foreach (var sourceType in authoring.observationSetup)
                {
                    sources.Add(new ObservationSource { SourceType = sourceType });
                }
                
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
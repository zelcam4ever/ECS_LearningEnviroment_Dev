using NUnit.Framework;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using UnityEngine.Serialization;

namespace EcsTraining
{
    public class AgentAutoring: MonoBehaviour
    {
        public string behaviourName = "a";
        public int maxStep;
        
        public ObservationSourceType[] observationSetup;
        
        [Tooltip("The number of continuous actions the agent can take.")]
        [Min(0)]
        public int numContinuousActions = 0;

        [Tooltip("An array defining the size of each discrete action branch.")]
        public int[] discreteBranchSizes;
        
        //Change from this component
        public Transform target;
        public Transform groundRender;
        
        private class Baker : Baker<AgentAutoring>
        {
            public override void Bake(AgentAutoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                // Agent baking
                AddComponent(entity, new AgentEcs() 
                {
                    MaxStep = authoring.maxStep,
                    EpisodeId = EpisodeIdCounter.GetEpisodeId(),
                    Target = GetEntity(authoring.target, TransformUsageFlags.Dynamic),
                    GoundRender = GetEntity(authoring.groundRender, TransformUsageFlags.None),
                });
                
                // Observation baking
                DynamicBuffer<ObservationValue> observations = AddBuffer<ObservationValue>(entity);
                observations.ResizeUninitialized(authoring.observationSetup.Length);
                
                DynamicBuffer<ObservationSource> sources = AddBuffer<ObservationSource>(entity);
                sources.Capacity = authoring.observationSetup.Length;
                foreach (var sourceType in authoring.observationSetup)
                {
                    sources.Add(new ObservationSource { SourceType = sourceType });
                }
                
                // Policy baking
                AddComponent(entity, new RemotePolicy());

                // Brain baking
                AddComponent(entity, new BrainSimple
                {
                    FullyQualifiedBehaviorName = authoring.behaviourName
                });
                
                // Actions runtime baking
                var actionComponent = new AgentAction();
                
                for (int i = 0; i < authoring.numContinuousActions; i++)
                {
                    actionComponent.ContinuousActions.Add(0f);
                }
                
                int numDiscreteBranches = authoring.discreteBranchSizes.Length;
                for (int i = 0; i < numDiscreteBranches; i++)
                {
                    actionComponent.DiscreteActions.Add(0);
                }
                AddComponent(entity, actionComponent);
        
                // Actions Spec baking
                var specComponent = new ActionsStructure
                {
                    NumContinuousActions = authoring.numContinuousActions
                };
                
                foreach (var branchSize in authoring.discreteBranchSizes)
                {
                    specComponent.DiscreteBranchSizes.Add(branchSize);
                }
        
                AddComponent(entity, specComponent);
            }
        }
    }

    
}
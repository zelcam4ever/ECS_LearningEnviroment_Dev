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
                var brainComponent = new BrainSimple();
                Assert.IsTrue(authoring.behaviourName.Length <= brainComponent.FullyQualifiedBehaviorName.Capacity,
                    $"The size of continuous actions ({authoring.behaviourName}) exceeds the capacity " +
                    $"of the FixedString ({brainComponent.FullyQualifiedBehaviorName}). " +
                    $"Please increase the FixedString size in the BrainSimple struct.");
                brainComponent.FullyQualifiedBehaviorName = authoring.behaviourName;
                AddComponent(entity, brainComponent);
                
                // Actions baking
                var actionComponent = new AgentAction();
                Assert.IsTrue(authoring.numContinuousActions <= actionComponent.ContinuousActions.Capacity,
                    $"The number of continuous actions ({authoring.numContinuousActions}) exceeds the capacity " +
                    $"of the FixedList ({actionComponent.ContinuousActions}). " +
                    $"Please increase the FixedList size in the AgentAction struct.");

                Assert.IsTrue(authoring.discreteBranchSizes.Length <= actionComponent.DiscreteActions.Capacity,
                    $"The number of discrete branches ({authoring.discreteBranchSizes.Length}) exceeds the capacity " +
                    $"of the FixedList ({actionComponent.DiscreteActions}). " +
                    $"Please increase the FixedList size in the AgentAction struct.");
                
                for (int i = 0; i < authoring.numContinuousActions; i++)
                {
                    actionComponent.ContinuousActions.Add(0f);
                }

                for (int i = 0; i < authoring.discreteBranchSizes.Length; i++)
                {
                    for (int j = 0; j < authoring.discreteBranchSizes[i]; j++)
                    {
                        actionComponent.DiscreteActions.Add(0);
                    }
                }
                Debug.Log("Lenght is: " + actionComponent.DiscreteActions.Length); ;
                AddComponent(entity, actionComponent);
            }
        }
    }

    
}
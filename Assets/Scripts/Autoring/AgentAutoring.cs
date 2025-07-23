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
        
        [Header("Observations")]
        public ObservationSourceType[] observationSetup;
        
        [Header("Actions")]
        [Tooltip("The number of continuous actions the agent can take.")]
        [Min(0)]
        public int numContinuousActions = 0;

        [Tooltip("An array defining the size of each discrete action branch.")]
        public int[] discreteBranchSizes;
        

        public bool decisionRequester;
        // These fields will only appear if the bool decisionRequester true
        
        /// <summary>
        /// The frequency with which the agent requests a decision. A DecisionPeriod of 5 means
        /// that the Agent will request a decision every 5 Academy steps. /// </summary>
        public int decisionPeriod = 5;
        
        /// <summary>
        /// The frequency with which the agent requests a decision. A DecisionPeriod of 5 means
        /// that the Agent will request a decision every 5 Academy steps. /// </summary>
        public int decisionStep = 0;
        
        /// <summary>
        /// Indicates whether the agent will take an action during the Academy steps where
        /// it does not request a decision. Has no effect when DecisionPeriod is set to 1.
        /// </summary>
        public bool takeActionsBetweenDecisions = true;
        
        
        //Change from this component
        [Header("Targets")]
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
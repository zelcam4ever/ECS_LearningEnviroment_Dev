using System.Collections.Generic;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static Unity.Entities.SystemAPI;

using Unity.MLAgents.CommunicatorObjects; // For ObservationProto, AgentInfoProto, etc.


namespace EcsTraining
{
    //RequestDecision() in RemotePolicy
    [UpdateAfter(typeof(ObservationCollectionSystem))]
    public partial class ExternalCommunicatorSystem : SystemBase
    {
        private List<ISensor> _sensors;
        private VectorSensor _vectorObservation;

        private bool _isInitialized;

        protected override void OnCreate()
        {
            // Subscribe all brains
            /*foreach (var (agent, policy, observations) in
                     Query<RefRW<AgentEcs>, RefRW<BrainSimple>, DynamicBuffer<ObservationValue>>()
                         .WithAll<RemotePolicy>())
            {

            }*/
            
            //Change
            _sensors = new List<ISensor> { null };
            _vectorObservation = new VectorSensor(4);
        }

        protected override void OnUpdate()
        {
            if(!_isInitialized)CommunicatorManager.SubscribeBrain("a", new ActionSpec(0, new int[]{4}));
            _isInitialized = true;
            
            foreach (var (agent, policy, observations) in
                     Query<RefRW<AgentEcs>, RefRW<BrainSimple>, DynamicBuffer<ObservationValue>>()
                         .WithAll<RemotePolicy>())
            {
                if (!agent.ValueRO.RequestDecision) continue;

                var observationArray = new float[observations.Length];
                for (int i = 0; i < observations.Length; i++)
                {
                    observationArray[i] = observations[i].Value;
                }
                
                _vectorObservation.Reset();
                _vectorObservation.AddObservation(observationArray);
                _sensors[0] = _vectorObservation;
                
                CommunicatorManager.PutObservation("a", agent.ValueRO, _sensors);
                
                agent.ValueRW.Reward = 0f;
                agent.ValueRW.GroupReward = 0f;
                agent.ValueRW.RequestDecision = false;
            }
        }
    }
}
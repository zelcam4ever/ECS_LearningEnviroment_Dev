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
    [UpdateAfter(typeof(ObservationCollectionSimpleSystem))]
    public partial class ExternalCommunicatorSystem : SystemBase
    {
        private List<ISensor> _sensors;
        private VectorSensor _vectorObservation;

        protected override void OnCreate()
        {
            
            CommunicatorManager.SubscribeBrain("a", new ActionSpec(0, new int[]{4}));
            Debug.Log("BrainSubscribed");
            
            // Subscribe all brains
            /*foreach (var (agent, policy, observations) in
                     Query<RefRW<AgentEcs>, RefRW<BrainSimple>, DynamicBuffer<ObservationValue>>()
                         .WithAll<RemotePolicy>())
            {

            }*/
            
            //Change
            _sensors = new List<ISensor> { null };
            _vectorObservation = new VectorSensor(6);
        }

        protected override void OnUpdate()
        {
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
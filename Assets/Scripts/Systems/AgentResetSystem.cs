using System.Collections.Generic;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(EpisodeCompletedSystem))]
    public partial class AgentResetSystem : SystemBase
    {
        private List<ISensor> _sensors;
        private VectorSensor _vectorObservation;

        protected override void OnCreate()
        {
            //Change
            _sensors = new List<ISensor> { null };
            _vectorObservation = new VectorSensor(4);
        }

        protected override void OnUpdate()
        {
            foreach (var (agent, policy, observations) in
                     Query<RefRW<AgentEcs>, RefRW<BrainSimple>, DynamicBuffer<ObservationValue>>()
                         .WithAll<RemotePolicy>())
            {
                if(!agent.ValueRO.Done) continue;
                
                if (agent.ValueRO.Reward > 50)
                {
                    Debug.Log($"Sending huge Reward in id:{agent.ValueRO.EpisodeId}");
                }

                var observationArray = new float[observations.Length];
                for (int i = 0; i < observations.Length; i++)
                {
                    observationArray[i] = observations[i].Value;
                }
                
                _vectorObservation.Reset();
                _vectorObservation.AddObservation(observationArray);
                _sensors[0] = _vectorObservation;
                
                CommunicatorManager.PutObservation("a", agent.ValueRO, _sensors);
                    
                Debug.Log("Resetting agent: " + agent.ValueRO.EpisodeId);
                
                agent.ValueRW.CompletedEpisodes += 1;
                agent.ValueRW.Reward = 0f;
                agent.ValueRW.GroupReward = 0f;
                agent.ValueRW.CumulativeReward = 0f;
                agent.ValueRW.RequestAction = false;
                agent.ValueRW.RequestDecision = false;
                agent.ValueRW.StepCount = 0;
                agent.ValueRW.Done = false;
                agent.ValueRW.MaxStepReached = false;
                
                agent.ValueRW.StartingEpisode = true;
            }
        }
    }
}
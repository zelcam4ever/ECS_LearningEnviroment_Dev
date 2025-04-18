using System.Collections.Generic;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    //RequestDecision() in RemotePolicy
    [UpdateAfter(typeof(SenInfoToBrainSystem))]
    public partial struct ExternalPolicySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
            CommunicatorManager.SubscribeBrain("a", new ActionSpec(0, new int[]{4}));
            Debug.Log("BrainSubscribed");
        }
    
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, policy, observation) in 
                     Query<RefRW<Agent>, RefRW<BrainSimple>, RefRO<Observation>>()
                    .WithAll<RemotePolicy>())
            {
                if (!agent.ValueRO.RequestDecision) continue;
                
                var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.EpisodeId);
                var sensors = new List<ISensor>();
                var vectorSensor = new VectorSensor(6);
                vectorSensor.AddObservation(observation.ValueRO.OwnPosition);
                vectorSensor.AddObservation(observation.ValueRO.TargetPosition);
                sensors.Add(vectorSensor);
                CommunicatorManager.PutObservation("a", agentInfo, sensors);
                
                agent.ValueRW.Reward = 0f;
                agent.ValueRW.GroupReward = 0f;
                agent.ValueRW.RequestDecision = false;
            }
        }
    }
}
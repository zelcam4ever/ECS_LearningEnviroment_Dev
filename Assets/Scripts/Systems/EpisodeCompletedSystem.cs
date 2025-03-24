using System.Collections.Generic;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;


namespace EcsTraining
{
    [UpdateAfter(typeof(RewardSystem))]
    public partial struct EpisodeCompletedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
    
        public void OnUpdate(ref SystemState state)
        {
            
            foreach (var (agent, entity) in Query<RefRW<Agent>>().WithPresent<AgentReset>().WithEntityAccess())
            {
                if (agent.ValueRO.StepCount >= agent.ValueRO.MaxStep && agent.ValueRO.MaxStep > 0)
                {
                    Debug.Log("Episode completed for agent: " + agent.ValueRO.AgentInfoId);
                    
                    var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId);
                    /*

                    agentInfo = new AgentInfo
                    {
                        episodeId = agent.ValueRO.EpisodeId,
                        reward = agent.ValueRO.Reward,
                        groupReward = agent.ValueRO.GroupReward,
                        done = true,
                        maxStepReached = true,
                        groupId = agent.ValueRO.GroupId
                    };
                    //TODO: Update sensors
                    //TODO: CollectObservations
                    //TODO: change, currently simulating:
                    var sensors = new List<ISensor>();
                    var vectorSensor = new VectorSensor(6);
                    vectorSensor.AddObservation(Vector3.right);
                    var targetPosition = GetComponent<LocalTransform>(agent.ValueRO.Target).Position;
                    vectorSensor.AddObservation(targetPosition);
                    sensors.Add(vectorSensor);
                    CommunicatorManager.PutObservation("a", agentInfo, sensors);
                    //TODO: DemonstrationWriter
                    //TODO: Reset sensors*/
                    
                    agent.ValueRW.CompletedEpisodes += 1;
                    agent.ValueRW.Reward = 0f;
                    agent.ValueRW.GroupReward = 0f;
                    agent.ValueRW.CumulativeReward = 0f;
                    agent.ValueRW.RequestAction = false;
                    agent.ValueRW.RequestDecision = false;
                    //agentInfo.storedActions.DiscreteActions.Clear();
                    //AgentInfoManager.SetAgentInfo(agent.ValueRO.AgentInfoId, agentInfo);
                    SetComponentEnabled<AgentReset>(entity, true);
                }
            }
        }
    }
}
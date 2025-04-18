using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.Rendering;
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
            
            foreach (var (agent,transform, entity) in Query<RefRW<Agent>, RefRO<LocalTransform>>().WithPresent<AgentReset>().WithEntityAccess())
            {
                var isDone = false;
                var maxSteps = false; //TODO: Changeplease
                if (math.distance(transform.ValueRO.Position,
                        GetComponent<LocalTransform>(agent.ValueRO.Target).Position) < 0.5f)
                {
                    Debug.Log("Episode completed [DONE] for agent: " + agent.ValueRO.EpisodeId);
                    isDone = true;
                    var material = GetComponent<MaterialMeshInfo>(agent.ValueRO.GoundRender);
                    material.Material = -1;
                    SetComponent(agent.ValueRO.GoundRender, material);
                    
                } else if (agent.ValueRO.StepCount >= agent.ValueRO.MaxStep && agent.ValueRO.MaxStep > 0)
                {
                    Debug.Log("Episode completed [STEP LIMIT] for agent: " + agent.ValueRO.EpisodeId);
                    isDone = true;
                    maxSteps = true;
                    
                    var material = GetComponent<MaterialMeshInfo>(agent.ValueRO.GoundRender);
                    material.Material = -2;
                    SetComponent(agent.ValueRO.GoundRender, material);
                }
                
                if(!isDone) return;
                var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.EpisodeId);
                

                agentInfo = new AgentInfo
                {
                    episodeId = agent.ValueRO.EpisodeId,
                    reward = agent.ValueRO.Reward,
                    groupReward = agent.ValueRO.GroupReward,
                    done = true,
                    maxStepReached = maxSteps,
                    groupId = agent.ValueRO.GroupId
                };
                if (agentInfo.reward > 50)
                {
                    Debug.Log($"Sending huge Reward in id:{agentInfo.episodeId}");
                }
                //TODO: Update sensors
                //TODO: CollectObservations
                //TODO: change, currently simulating:
                var sensors = new List<ISensor>();
                var vectorSensor = new VectorSensor(6);
                vectorSensor.AddObservation(transform.ValueRO.Position);
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
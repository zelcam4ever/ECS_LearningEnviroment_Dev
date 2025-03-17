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
            CommunicatorManager.SubscribeBrain("a", new ActionSpec());
            
        }
    
        public void OnUpdate(ref SystemState state)
        {
            foreach ((var agent, var policy) in 
                     Query<RefRO<Agent>, RefRW<BrainSimple>>()
                    .WithAll<RemotePolicy>())
            {
                policy.ValueRW.AgentId = agent.ValueRO.EpisodeId;
                
                CommunicatorManager.PutObservation(policy.ValueRO.FullyQualifiedBehaviorName.Value,AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId),new List<ISensor>());
            }
        }
    }
}
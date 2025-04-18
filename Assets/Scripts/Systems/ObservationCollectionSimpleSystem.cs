using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(IncrementStepSystem))]
    public partial struct ObservationCollectionSimpleSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, transform, observation) in Query<RefRW<Agent>,RefRO<LocalTransform>,RefRW<Observation>>())
            {
                if(!agent.ValueRO.RequestDecision) continue;
                
                var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.EpisodeId);
                if (agentInfo.done)
                {
                    agentInfo.ClearActions();
                }
                else
                {
                    //agentInfo.CopyActions(m_ActuatorManager.StoredActions);
                }
                AgentInfoManager.SetAgentInfo(agent.ValueRO.EpisodeId, agentInfo);
                
                //TODO: Change observation system to observation components + parallelJob
                observation.ValueRW.OwnPosition = transform.ValueRO.Position;
                observation.ValueRW.TargetPosition = GetComponent<LocalTransform>(agent.ValueRO.Target).Position;
            }
        }
    }
}
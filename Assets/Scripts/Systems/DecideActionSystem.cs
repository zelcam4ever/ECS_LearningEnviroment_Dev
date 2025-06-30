using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(ExternalPolicySystem))]
    public partial struct DecideActionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Training>();
            state.RequireForUpdate<BrainSimple>();
        }

        private int time;
        
        public void OnUpdate(ref SystemState state)
        {
            foreach ((var agent, var brain, var action) in Query<RefRW<Agent>, RefRO<BrainSimple>,RefRW<Action>>())
            {
                /*if (m_ActuatorManager.StoredActions.ContinuousActions.Array == null)
                {
                    ResetData();
                }*/
                //var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId);

                var actionBufferToCopy = CommunicatorManager.DecideAction(brain.ValueRO.FullyQualifiedBehaviorName.Value,agent.ValueRO.EpisodeId);
                
                Debug.Log($"Agent {agent.ValueRO.EpisodeId}: action taken: {actionBufferToCopy.DiscreteActions[0]}");
                
                //Not necessary: Only important for logging
                //agentInfo.CopyActions(actionBufferToCopy);
                
                //TODO Change / update?
                action.ValueRW.Value = actionBufferToCopy.DiscreteActions[0];
                
                //agentInfo.CopyActions(actionBufferToCopy);
                //AgentInfoManager.SetAgentInfo(agent.ValueRO.AgentInfoId, agentInfo);
                // m_ActuatorManager.UpdateActions(actions);
                //m_ActuatorManager.UpdateActions(actions);
            }
        }
    }
}
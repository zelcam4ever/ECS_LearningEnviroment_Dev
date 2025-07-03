using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(ExternalCommunicatorSystem))]
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
            foreach (var (agent, brain, action) in Query<RefRO<AgentEcs>, RefRO<BrainSimple>,RefRW<Action>>())
            {
                /*if (m_ActuatorManager.StoredActions.ContinuousActions.Array == null)
                {
                    ResetData();
                }*/
                //var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId);

                var actionBufferToCopy = CommunicatorManager.DecideAction(brain.ValueRO.FullyQualifiedBehaviorName.Value,agent.ValueRO.EpisodeId);
                
                /*if (actionBufferToCopy.IsEmpty())
                {
                    Debug.Log($"Agent {agent.ValueRO.EpisodeId}: No action taken!");
                    continue;
                }*/
                //Debug.Log($"Agent {agent.ValueRO.EpisodeId}: action taken: {actionBufferToCopy.DiscreteActions[0]}");
                
                //Not necessary: Only important for logging
                //agentInfo.CopyActions(actionBufferToCopy);
                
                //TODO Change / update?
                if (actionBufferToCopy.DiscreteActions.Length > 0)
                {
                    action.ValueRW.Value = actionBufferToCopy.DiscreteActions[0];
                }
                else action.ValueRW.Value = 0;



                //agentInfo.CopyActions(actionBufferToCopy);
                //AgentInfoManager.SetAgentInfo(agent.ValueRO.AgentInfoId, agentInfo);
                // m_ActuatorManager.UpdateActions(actions);
                //m_ActuatorManager.UpdateActions(actions);
            }
        }
    }
}
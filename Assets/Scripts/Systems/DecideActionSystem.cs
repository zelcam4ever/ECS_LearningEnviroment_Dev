using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(ExternalCommunicatorSystem))]
    public partial class DecideActionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var (agent, brain, actionComponent) in Query<RefRO<AgentEcs>, RefRO<BrainSimple>, RefRW<AgentAction>>())
            {
                //if(someReason) continue;
                
                /*if (m_ActuatorManager.StoredActions.ContinuousActions.Array == null)
                {
                    ResetData();
                }*/
                var actionBufferToCopy =
                    CommunicatorManager.DecideAction(brain.ValueRO.FullyQualifiedBehaviorName.Value,
                        agent.ValueRO.EpisodeId);

                if (actionBufferToCopy.Equals(ActionBuffers.Empty))
                {
                    Debug.Log($"Agent {agent.ValueRO.EpisodeId}: No action taken!");
                    continue;
                }
                //Debug.Log($"Agent {agent.ValueRO.EpisodeId}: action taken: {actionBufferToCopy.DiscreteActions[0]}");

                //Not necessary: Only important for logging
                //agentInfo.CopyActions(actionBufferToCopy);
                
                actionComponent.ValueRW.ContinuousActions.Clear();
                actionComponent.ValueRW.DiscreteActions.Clear();
                
                foreach (var actionValue in actionBufferToCopy.ContinuousActions.Array)
                {
                    actionComponent.ValueRW.ContinuousActions.Add(actionValue);
                }

                foreach (var actionValue in actionBufferToCopy.DiscreteActions.Array)
                {
                    actionComponent.ValueRW.DiscreteActions.Add(actionValue);
                }
                
                //agentInfo.CopyActions(actionBufferToCopy);
                //AgentInfoManager.SetAgentInfo(agent.ValueRO.AgentInfoId, agentInfo);
                // m_ActuatorManager.UpdateActions(actions);
                //m_ActuatorManager.UpdateActions(actions);
            }
        }
    }
}
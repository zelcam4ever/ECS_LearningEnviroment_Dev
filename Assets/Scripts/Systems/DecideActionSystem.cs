using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    public partial struct DecideActionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Training>();
            state.RequireForUpdate<BrainSimple>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach ((var agent, var brain) in Query<RefRW<Agent>, RefRO<BrainSimple>>())
            {
                /*if (m_ActuatorManager.StoredActions.ContinuousActions.Array == null)
                {
                    ResetData();
                }*/
                var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId);

                var actionBufferToCopy = CommunicatorManager.DecideAction(brain.ValueRO.FullyQualifiedBehaviorName.Value,agent.ValueRO.AgentInfoId);
                agentInfo.CopyActions(actionBufferToCopy);

                AgentInfoManager.SetAgentInfo(agent.ValueRO.AgentInfoId, agentInfo);
                // m_ActuatorManager.UpdateActions(actions);
                //m_ActuatorManager.UpdateActions(actions);
            }
        }
    }
}
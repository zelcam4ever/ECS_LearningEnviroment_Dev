using Unity.Burst;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using static Unity.Entities.SystemAPI;


namespace EcsTraining
{
    [UpdateAfter(typeof(IncrementStepSystem))]
    public partial struct SenInfoToBrainSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>(); //?
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var agent in Query<RefRW<Agent>>())
            {
                if (!agent.ValueRO.RequestDecision) return;
                //SendInfoToBrain() start:
                //var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId);
                /*if (agentInfo.done)
                {
                    //agent.ValueRW.InfoBrain ClearActions()
                }*/
                else
                {
                    //agent.CopyActions(m_ActuatorManager.StoredActions);
                }
                
                //JOB: UpdateAllSensors(); && Collect all the Observations TODO: invedstigate how
                
                //m_ActuatorManager.WriteActionMask();
                //agentInfo.DiscreteActionMasks = m_ActuatorManager.DiscreteActionMask?.GetMask();
                /*agentInfo.reward = agent.ValueRO.Reward;
                agentInfo.groupReward = agent.ValueRO.GroupReward;
                agentInfo.done = false;
                agentInfo.maxStepReached = false;
                agentInfo.episodeId = agent.ValueRO.EpisodeId;
                agentInfo.groupId= agent.ValueRO.GroupId;*/

                var agentInfo = AgentInfoManager.GetAgentInfo(agent.ValueRO.AgentInfoId);

                agentInfo = new AgentInfo
                {
                    reward = agent.ValueRO.Reward,
                    groupReward = agent.ValueRO.GroupReward,
                    done = false,
                    maxStepReached = false,
                    episodeId = agent.ValueRO.EpisodeId,
                    groupId = agent.ValueRO.GroupId
                };

                AgentInfoManager.SetAgentInfo(agent.ValueRO.AgentInfoId, agentInfo);

                //JOB: m_Brain.RequestDecision(m_Info, sensors);

                //Maybe agent.Entity.Brain? 

                //DemonstrationWriters?

                //SendInfoToBrain() end:


            }
        }


        /*[BurstCompile]
        public partial struct IncrementAgentStepJob : IJobEntity
        {
            
        }*/
    }
}
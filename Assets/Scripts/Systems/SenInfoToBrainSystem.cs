using Unity.Burst;
using Unity.Entities;
using Unity.MLAgents.Actuators;
using static Unity.Entities.SystemAPI;


namespace EcsTraining
{
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
                if (!agent.ValueRO.RequestAction) return;
                //SendInfoToBrain() start:
                
                if (agent.ValueRO.InfoBrain.Done)
                {
                    //agent.ValueRW.InfoBrain ClearActions()
                }
                else
                {
                    //agent.CopyActions(m_ActuatorManager.StoredActions);
                }
                
                //JOB: UpdateAllSensors(); && Collect all the Observations TODO: invedstigate how
                
                //m_ActuatorManager.WriteActionMask();
                //agent.ValueRW.InfoBrain.DiscreteActionMasks = m_ActuatorManager.DiscreteActionMask?.GetMask();
                agent.ValueRW.InfoBrain.Reward = agent.ValueRO.Reward;
                agent.ValueRW.InfoBrain.GroupReward = agent.ValueRO.GroupReward;
                agent.ValueRW.InfoBrain.Done = false;
                agent.ValueRW.InfoBrain.MaxStepReached = false;
                agent.ValueRW.InfoBrain.EpisodeId = agent.ValueRO.EpisodeId;
                agent.ValueRW.InfoBrain.GroupId= agent.ValueRO.GroupId;
                
                //JOB: m_Brain.RequestDecision(m_Info, sensors);
                
                //DemonstrationWriters?
                
                //SendInfoToBrain() end:
                
                agent.ValueRW.Reward = 0;
                agent.ValueRW.GroupReward = 0;
                agent.ValueRW.RequestAction = false;
                
                
            }
        }


        /*[BurstCompile]
        public partial struct IncrementAgentStepJob : IJobEntity
        {
            
        }*/
    }
}
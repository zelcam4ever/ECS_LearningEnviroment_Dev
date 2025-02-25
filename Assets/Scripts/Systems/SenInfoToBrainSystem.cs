using Unity.Burst;
using Unity.Entities;
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

                if (agent.ValueRO.InfoBrain.Done)
                {
                    //agent.ValueRW.InfoBrain ClearActions()
                }
                else
                {
                    //agent.CopyActions(m_ActuatorManager.StoredActions);
                }
                agent.ValueRW.Reward = 0;
                agent.ValueRW.GroupReward = 0;
                agent.ValueRW.RequestAction = false;
                
                //UpdateAllSensors();
            }
        }


        /*[BurstCompile]
        public partial struct IncrementAgentStepJob : IJobEntity
        {
            
        }*/
    }
}
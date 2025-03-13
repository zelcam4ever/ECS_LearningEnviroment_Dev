using Unity.Entities;
using Unity.VisualScripting;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(DecideActionSystem))]
    public partial struct AgentActionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Training>(); 
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var agent in Query<RefRW<Agent>>())
            {
                if (agent.ValueRO.RequestAction) //&& and brain!=null
                {
                    agent.ValueRW.RequestAction = false;
                    //ActuatorManager.ExecuteActions();
                }

                if (agent.ValueRO.StepCount >= agent.ValueRO.MaxStep && agent.ValueRO.MaxStep > 0)
                {
                    //NotifyAgentDone(DoneReason.MaxStepReached);
                    //_AgentReset()
                }
            }
        }
    }
}
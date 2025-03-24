using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
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
            foreach (var (agent, action) in Query<RefRW<Agent>, RefRO<Action>>())
            {
                if (agent.ValueRO.RequestAction) //&& and brain!=null
                {
                    agent.ValueRW.RequestAction = false;
                    //ActuatorManager.ExecuteActions();
                    Debug.Log("Im doing the following action: " +action.ValueRO.Value);
                }

                
            }
        }
    }
}
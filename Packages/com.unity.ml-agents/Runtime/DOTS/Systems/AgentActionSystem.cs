using Unity.Entities;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;
using Random = UnityEngine.Random;

namespace MLAgents.DOTS
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
            foreach (var (agent, action, actionSpec, transform) in 
                     Query<RefRW<AgentEcs>, RefRO<AgentAction>, RefRO<ActionsStructure>, RefRW<LocalTransform>>())
            {
                if (!agent.ValueRO.RequestAction) continue; //&& and brain!=null
                
                agent.ValueRW.RequestAction = false;
                //ActuatorManager.ExecuteActions();
                var actionsTaking = action.ValueRO.DiscreteActions;
                if(actionsTaking.Length == 0) continue;
                    
                float3 movement;
                switch (actionsTaking[0])
                {
                    case 0:
                        movement = new float3(1,0, 0);
                        break;
                    case 1:
                        movement = new float3(-1,0,0);
                        break;
                    case 2:
                        movement = new float3(0, 0, 1);
                        break;
                    case 3:
                        movement = new float3(0, 0, -1);
                        break;
                    default:
                        movement = float3.zero;
                        Debug.LogWarning("Action out of range");
                        break;
                }

                transform.ValueRW.Position += movement;


            }
        }
    }
}
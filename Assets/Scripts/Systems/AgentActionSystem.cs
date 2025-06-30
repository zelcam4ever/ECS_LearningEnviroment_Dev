using Unity.Entities;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Entities.SystemAPI;
using Random = UnityEngine.Random;

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
            foreach (var (agent, action, transform) in Query<RefRW<AgentEcs>, RefRO<Action>, RefRW<LocalTransform>>())
            {
                if (agent.ValueRO.RequestAction) //&& and brain!=null
                {
                    agent.ValueRW.RequestAction = false;
                    //ActuatorManager.ExecuteActions();
                    Debug.Log("Im doing the following action: " +action.ValueRO.Value);
                    var movement = new float3();
                    switch (action.ValueRO.Value)
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
                            Debug.LogWarning("Action out of range");
                            break;
                    }

                    transform.ValueRW.Position += movement;
                }

                
            }
        }
    }
}
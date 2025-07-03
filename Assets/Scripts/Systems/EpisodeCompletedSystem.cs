using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;


namespace EcsTraining
{
    [UpdateAfter(typeof(RewardSystem))]
    public partial struct EpisodeCompletedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
    
        public void OnUpdate(ref SystemState state)
        {
            
            foreach (var (agent,observation) in Query<RefRW<AgentEcs>, DynamicBuffer<ObservationValue>>())
            {
                var isDone = false;
                var maxSteps = false; //TODO: Changeplease
                
                if (math.distance(new float2(observation[0],observation[1]),new float2(observation[2],observation[3])) < 0.5f)
                {
                    Debug.Log("Episode completed [DONE] for agent: " + agent.ValueRO.EpisodeId);
                    isDone = true;
                    var material = GetComponent<MaterialMeshInfo>(agent.ValueRO.GoundRender);
                    material.Material = -1;
                    SetComponent(agent.ValueRO.GoundRender, material);
                    
                } else if (agent.ValueRO.StepCount >= agent.ValueRO.MaxStep && agent.ValueRO.MaxStep > 0)
                {
                    Debug.Log("Episode completed [STEP LIMIT] for agent: " + agent.ValueRO.EpisodeId);
                    isDone = true;
                    maxSteps = true;
                    
                    var material = GetComponent<MaterialMeshInfo>(agent.ValueRO.GoundRender);
                    material.Material = -2;
                    SetComponent(agent.ValueRO.GoundRender, material);
                }
                

                agent.ValueRW.Done = isDone;
                agent.ValueRW.MaxStepReached = maxSteps;
            }
        }
    }
}
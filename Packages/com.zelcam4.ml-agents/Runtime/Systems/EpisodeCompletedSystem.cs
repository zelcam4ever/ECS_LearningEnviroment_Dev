using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Zelcam4.MLAgents;

using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;


namespace Zelcam4.MLAgents
{
    [UpdateAfter(typeof(RewardSystem))]
    public partial struct EpisodeCompletedSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent,observation) in Query<RefRW<AgentEcs>, DynamicBuffer<ObservationValue>>())
            {
                if (math.distance(new float2(observation[0],observation[1]),new float2(observation[2],observation[3])) < 0.5f)
                {
                    agent.ValueRW.Done = true;
                    
                    UpdateMeshMaterial(ref state, agent.ValueRO, true);
                    
                } else if (agent.ValueRO.StepCount >= agent.ValueRO.MaxStep && agent.ValueRO.MaxStep > 0)
                {
                    agent.ValueRW.Done = true;
                    agent.ValueRW.MaxStepReached = true;
                    
                    UpdateMeshMaterial(ref state, agent.ValueRO, false);
                }
            }
        }

        private void UpdateMeshMaterial(ref SystemState state, AgentEcs agent, bool succeeded)
        {
            var material = GetComponent<MaterialMeshInfo>(agent.GoundRender);
            material.Material = succeeded? -1 : -2;
            SetComponent(agent.GoundRender, material);
        }
    }
}
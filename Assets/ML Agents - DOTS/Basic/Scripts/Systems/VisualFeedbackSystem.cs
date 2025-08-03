using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using Zelcam4.MLAgents;
using static Unity.Entities.SystemAPI;

namespace Sample.Scripts
{
    [BurstCompile]
    [UpdateInGroup(typeof(PreAgentResetSystemGroup))]
    public partial struct VisualFeedbackSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, visualFeedback) in Query<RefRO<AgentEcs>, RefRO<AgentVisualFeedback>>().WithAll<EndEpisodeTag>())
            {
                UpdateMeshMaterial(ref state, visualFeedback.ValueRO, agent.ValueRO.MaxStepReached);
            }
        }

        private void UpdateMeshMaterial(ref SystemState state, AgentVisualFeedback target, bool maxSteps)
        {
            var material = GetComponent<MaterialMeshInfo>(target.RendererEntity);
            material.Material = maxSteps? -2 : -1;
            SetComponent(target.RendererEntity, material);
        }
    }
    
}
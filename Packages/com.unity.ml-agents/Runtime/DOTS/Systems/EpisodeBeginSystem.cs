using Unity.Entities;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;
using Random = UnityEngine.Random;

namespace MLAgents.DOTS
{
    [UpdateAfter(typeof(AgentResetSystem))]
    public partial struct EpisodeBeginSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
    
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, transform) in Query<RefRW<AgentEcs>, RefRW<LocalTransform>>())
            {
                if(!agent.ValueRO.StartingEpisode) continue;
                agent.ValueRW.StartingEpisode = false;
                
                transform.ValueRW.Position = new float3(0, 0, Mathf.Ceil(Random.Range(-2f,2f)));
                
                var targetTransform = GetComponent<LocalTransform>(agent.ValueRO.Target);
                targetTransform.Position = new float3(Mathf.Ceil(Random.Range(5f, 8f)), 0, Mathf.Ceil(Random.Range(-3f,3f)));;
                SetComponent(agent.ValueRO.Target, targetTransform);
            }
        }
    }
}
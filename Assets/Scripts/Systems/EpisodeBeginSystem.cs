using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;
using Random = UnityEngine.Random;

namespace EcsTraining
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
            foreach (var (agent, transform, entity) in Query<RefRW<Agent>, RefRW<LocalTransform>>().WithAll<OnEpisodeBegin>().WithEntityAccess())
            {
                transform.ValueRW.Position = new float3(0, 0, Mathf.Ceil(Random.Range(-2f,2f)));
                var targetTransform = GetComponent<LocalTransform>(agent.ValueRO.Target);
                
                targetTransform.Position = new float3(Mathf.Ceil(Random.Range(5f, 8f)), 0, Mathf.Ceil(Random.Range(-3f,3f)));;
                SetComponent(agent.ValueRO.Target, targetTransform);
                
                SetComponentEnabled<OnEpisodeBegin>(entity, false);
            }
        }
    }
}
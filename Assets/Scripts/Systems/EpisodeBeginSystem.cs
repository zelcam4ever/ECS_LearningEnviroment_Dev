using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
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
                transform.ValueRW.Position = new float3(Random.Range(-3f, 1f), 0, Random.Range(-2f,2f));
                var targetTransform = GetComponent<LocalTransform>(agent.ValueRO.Target);
                
                targetTransform.Position = new float3(Random.Range(2.5f, 5f), 0, Random.Range(-2f,2f));;
                SetComponent(agent.ValueRO.Target, targetTransform);
                
                SetComponentEnabled<OnEpisodeBegin>(entity, false);
            }
        }
    }
}
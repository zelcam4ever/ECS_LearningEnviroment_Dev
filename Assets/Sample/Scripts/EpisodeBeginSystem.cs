using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Zelcam4.MLAgents;
using static Unity.Entities.SystemAPI;
using Random = UnityEngine.Random;

namespace Sample.Scripts
{
    [UpdateAfter(typeof(InitializeEnvironmentSystem))]
    [UpdateBefore(typeof(RequesterSystem))]
    public partial struct EpisodeBeginSystem : ISystem
    {
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, transform, customObservation) in 
                     Query<RefRW<AgentEcs>,  RefRW<LocalTransform>, RefRW<CustomObservation>>())
            {
                if(!agent.ValueRO.StartingEpisode) continue;
                agent.ValueRW.StartingEpisode = false;
                
                transform.ValueRW.Position = new float3(0, 0, Mathf.Ceil(Random.Range(-2f,2f)));
                
                var targetTransform = GetComponent<LocalTransform>(customObservation.ValueRO.Target);
                targetTransform.Position = new float3(Mathf.Ceil(Random.Range(5f, 8f)), 0, Mathf.Ceil(Random.Range(-3f,3f)));;
                SetComponent(customObservation.ValueRO.Target, targetTransform);
            }
        }
    }
}
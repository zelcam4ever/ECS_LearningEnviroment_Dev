using NUnit.Framework;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Zelcam4.MLAgents;

namespace Sample.Scripts
{
    [UpdateInGroup(typeof(EpisodeCompletedGroup))]
    public partial struct EpisodeCompletedSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            var job = new GoalReachedJob
            {
                ECB = ecb.AsParallelWriter()
            };
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    public partial struct GoalReachedJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute([ChunkIndexInQuery] int entityInQueryIndex, Entity entity, ref AgentEcs agent, in LocalTransform transform, in CustomObservation observation)
        {
            var targetPosition = observation.Position;
            if (math.distance(transform.Position, targetPosition) < 0.5f)
            {
                ECB.AddComponent<EndEpisodeTag>(entityInQueryIndex, entity);
            }
        }
    }
}
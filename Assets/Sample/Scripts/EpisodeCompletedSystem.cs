using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
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
            
            var job = new TerminateOnMaxStepsJob
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

        private void Execute([ChunkIndexInQuery] int entityInQueryIndex, Entity entity, ref AgentEcs agent, in DynamicBuffer<ObservationValue> observations)
        {
            if (math.distance(new float2(observations[0].Value, observations[1].Value), 
                    new float2(observations[2].Value, observations[3].Value)) < 0.5f)
            {
                agent.MaxStepReached = false;
                ECB.AddComponent<EndEpisodeTag>(entityInQueryIndex, entity);
            }
        }
    }
}
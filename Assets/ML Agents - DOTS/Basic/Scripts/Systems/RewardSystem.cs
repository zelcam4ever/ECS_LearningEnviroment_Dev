using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Zelcam4.MLAgents;
using static Unity.Entities.SystemAPI;

namespace Sample.Scripts
{
    [UpdateInGroup(typeof(RewardGroup))]
    [BurstCompile]
    public partial struct RewardSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new CalculateRewardJob();
        
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    public partial struct CalculateRewardJob : IJobEntity
    {
        private void Execute(ref AgentEcs agent, in LocalTransform transform, in CustomObservation observation)
        {
            var reward = 0f;
            
            var targetPosition = observation.Position;
            if (math.distance(transform.Position, targetPosition) < 0.5f)
            {
                reward = 100f;
            }
            else
            {
                reward = -0.1f;
            }

            agent.Reward += reward;
            agent.CumulativeReward += reward;
        }
    }
}
using Unity.Burst;
using Unity.Entities;

namespace Zelcam4.MLAgents
{
    [UpdateAfter(typeof(EpisodeCompleteGroup))]
    [BurstCompile]
    public partial struct AgentIsDoneSystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new SetDoneFlagJob();
            
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    public partial struct SetDoneFlagJob : IJobEntity
    {
        private void Execute(ref AgentEcs agent, in EpisodeCompletedTag tag)
        {
            agent.Done = true;
        }
    }
}
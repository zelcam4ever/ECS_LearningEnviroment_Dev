using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Zelcam4.MLAgents;

namespace Sample.Scripts
{
    [UpdateInGroup(typeof(ActionSystemGroup))]
    public partial struct AgentActionSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new AgentActionJob();
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    public partial struct AgentActionJob : IJobEntity
    {
        private void Execute(in AgentEcs agent, in AgentAction action, in RequestActionTag tag, ref LocalTransform transform)
        {
            var actionsTaking = action.DiscreteActions;
            
            float3 movement;
            if (actionsTaking.Length <= 0) return;
            switch (actionsTaking[0])
            {
                case 0: movement = new float3(1, 0, 0); break;
                case 1: movement = new float3(-1, 0, 0); break;
                case 2: movement = new float3(0, 0, 1); break;
                case 3: movement = new float3(0, 0, -1); break;
                default: movement = float3.zero; break;
            }
            transform.Position += movement;

        }
    }
}
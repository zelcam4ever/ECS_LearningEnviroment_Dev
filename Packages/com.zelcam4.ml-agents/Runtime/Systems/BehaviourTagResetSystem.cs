using Unity.Entities;

namespace Zelcam4.MLAgents
{
    
    [UpdateAfter(typeof(AgentResetSystem))]
    public partial struct BehaviourTagResetSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.World.Unmanaged);
            
            foreach (var (agent, entity) in 
                     SystemAPI.Query<RefRO<AgentEcs>>().WithEntityAccess()
                         .WithAny<RequestDecisionTag, RequestActionTag>())
            {
                ecb.RemoveComponent<RequestDecisionTag>(entity);
                ecb.RemoveComponent<RequestActionTag>(entity);
            }
        }
    }
}
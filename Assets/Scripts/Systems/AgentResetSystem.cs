using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(EpisodeCompletedSystem))]
    public partial struct AgentResetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
    
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, entity) in Query<RefRW<AgentEcs>>().WithAll<AgentReset>().WithEntityAccess())
            {
                //TODO: ResetData();
                Debug.Log("Reseted agent: " + agent.ValueRO.EpisodeId);
                agent.ValueRW.StepCount = 0;
                SetComponentEnabled<AgentReset>(entity, false);
                //TODO:OnEpisodeBeginSystem BETTER
                SetComponentEnabled<OnEpisodeBegin>(entity, true);
            }
        }
    }
}
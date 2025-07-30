using System.Collections.Generic;
using Unity.Entities;
using Zelcam4.MLAgents;

using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace Zelcam4.MLAgents
{
    [UpdateAfter(typeof(EpisodeCompletedSystem))]
    public partial class AgentResetSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var (agent, policy, observations) in
                     Query<RefRW<AgentEcs>, RefRO<BrainSimple>, DynamicBuffer<ObservationValue>>()
                         .WithAll<RemotePolicy>())
            {
                if(!agent.ValueRO.Done) continue;
                
                CommunicatorManager.PutObservation(policy.ValueRO.FullyQualifiedBehaviorName.Value, agent.ValueRO, observations);
                
                agent.ValueRW.CompletedEpisodes += 1;
                agent.ValueRW.Reward = 0f;
                agent.ValueRW.GroupReward = 0f;
                agent.ValueRW.CumulativeReward = 0f;
                agent.ValueRW.RequestAction = false;
                agent.ValueRW.RequestDecision = false;
                agent.ValueRW.StepCount = 0;
                agent.ValueRW.Done = false;
                agent.ValueRW.MaxStepReached = false;
                
                agent.ValueRW.StartingEpisode = true;
            }
        }
    }
}
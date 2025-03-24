using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateBefore(typeof(SenInfoToBrainSystem))]
    public partial struct ObservationCollectionSimpleSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>();
            state.RequireForUpdate<Training>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (agent, transform, observation) in Query<RefRW<Agent>,RefRO<LocalTransform>,RefRW<Observation>>())
            {
                if(!agent.ValueRO.RequestDecision) continue;
                
                //TODO: Change observation system to observation components + parallelJob
                observation.ValueRW.OwnPosition = transform.ValueRO.Position;
                observation.ValueRW.TargetPosition = GetComponent<LocalTransform>(agent.ValueRO.Target).Position;
            }
        }
    }
}
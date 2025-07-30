using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Zelcam4.MLAgents;

[BurstCompile]
[UpdateInGroup(typeof(ObservationCollectionGroup))]
public partial struct ExampleTransformObservationSystem  : ISystem
{
    private EntityQuery _query;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _query = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<ObservationValue, ObservationRequest<LocalTransform>, LocalTransform>()
            .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new GatherJob<LocalTransform, TransformExtractor>
        {
            FinalObservationBufferHandle = state.GetBufferTypeHandle<ObservationValue>(false),
            RequestsHandle = state.GetBufferTypeHandle<ObservationRequest<LocalTransform>>(true),
            SourceComponentHandle = state.GetComponentTypeHandle<LocalTransform>(true),
            Extractor = default
        };
        
        state.Dependency = job.ScheduleParallel(_query, state.Dependency);
    }
}
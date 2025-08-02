using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Zelcam4.MLAgents;

[BurstCompile]
[UpdateBefore(typeof(ObservationCollectionGroup))]
public partial struct ObservationSystem  : ISystem
{
    private EntityQuery _queryTransform;
    private EntityQuery _queryTargetInformation;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _queryTransform = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<ObservationValue, ObservationRequest<LocalTransform>, LocalTransform>()
            .Build(ref state);
        
        _queryTargetInformation = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<ObservationValue, ObservationRequest<CustomObservation>, CustomObservation>()
            .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Updating the self transform observation
        var jobTransform = new GatherJob<LocalTransform, TransformExtractor>
        {
            FinalObservationBufferHandle = state.GetBufferTypeHandle<ObservationValue>(false),
            RequestsHandle = state.GetBufferTypeHandle<ObservationRequest<LocalTransform>>(true),
            SourceComponentHandle = state.GetComponentTypeHandle<LocalTransform>(true),
            Extractor = default
        };
        
        state.Dependency = jobTransform.ScheduleParallel(_queryTransform, state.Dependency);
        
        // Updating a custom observation
        var jobTargetInformation = new GatherJob<CustomObservation, CustomObservationExtractor>
        {
            FinalObservationBufferHandle = state.GetBufferTypeHandle<ObservationValue>(false),
            RequestsHandle = state.GetBufferTypeHandle<ObservationRequest<CustomObservation>>(true),
            SourceComponentHandle = state.GetComponentTypeHandle<CustomObservation>(true),
            Extractor = default
        };
        
        state.Dependency = jobTargetInformation.ScheduleParallel(_queryTargetInformation, state.Dependency);
    }
}
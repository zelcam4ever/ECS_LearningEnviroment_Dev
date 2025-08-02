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

    // Declare the TypeHandles
    private BufferTypeHandle<ObservationValue> _valueBufferHandle;
    private BufferTypeHandle<ObservationRequest<LocalTransform>> _transformRequestHandle;
    private ComponentTypeHandle<LocalTransform> _transformHandle;
    private BufferTypeHandle<ObservationRequest<CustomObservation>> _customRequestHandle;
    private ComponentTypeHandle<CustomObservation> _customHandle;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _queryTransform = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<ObservationValue, ObservationRequest<LocalTransform>, LocalTransform>()
            .Build(ref state);
        
        _queryTargetInformation = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<ObservationValue, ObservationRequest<CustomObservation>, CustomObservation>()
            .Build(ref state);
        
        // Initialize the TypeHandles
        _valueBufferHandle = state.GetBufferTypeHandle<ObservationValue>(false);
        _transformRequestHandle = state.GetBufferTypeHandle<ObservationRequest<LocalTransform>>(true);
        _transformHandle = state.GetComponentTypeHandle<LocalTransform>(true);
        _customRequestHandle = state.GetBufferTypeHandle<ObservationRequest<CustomObservation>>(true);
        _customHandle = state.GetComponentTypeHandle<CustomObservation>(true);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Update the TypeHandles
        _valueBufferHandle.Update(ref state);
        _transformRequestHandle.Update(ref state);
        _transformHandle.Update(ref state);
        _customRequestHandle.Update(ref state);
        _customHandle.Update(ref state);
        
        var jobTransform = new GatherJob<LocalTransform, TransformExtractor>
        {
            FinalObservationBufferHandle = _valueBufferHandle,
            RequestsHandle = _transformRequestHandle,
            SourceComponentHandle = _transformHandle,
            Extractor = default
        };
        state.Dependency = jobTransform.ScheduleParallel(_queryTransform, state.Dependency);
        
        var jobTargetInformation = new GatherJob<CustomObservation, CustomObservationExtractor>
        {
            FinalObservationBufferHandle = _valueBufferHandle,
            RequestsHandle = _customRequestHandle,
            SourceComponentHandle = _customHandle,
            Extractor = default
        };
        state.Dependency = jobTargetInformation.ScheduleParallel(_queryTargetInformation, state.Dependency);
    }
}
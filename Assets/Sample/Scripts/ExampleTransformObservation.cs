using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Zelcam4.MLAgents;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(ObservationCollectionGroup))]
public partial class ExampleVelocityObservationSystem : SystemBase
{
    private EntityQuery _queryTransform;
    
    protected override void OnCreate()
    {
        _queryTransform = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<ObservationValue, ObservationRequest<LocalTransform>, LocalTransform>()
            .Build(this);
    }


    protected override void OnUpdate()
    {
        var jobTransform = new GatherJob<LocalTransform, TransformExtractor>
        {
            FinalObservationBufferHandle = GetBufferTypeHandle<ObservationValue>(false),
            RequestsHandle = GetBufferTypeHandle<ObservationRequest<LocalTransform>>(true),
            SourceComponentHandle = GetComponentTypeHandle<LocalTransform>(true),
            Extractor = default
        };
        
        Dependency = jobTransform.ScheduleParallel(_queryTransform, Dependency);

    }
}
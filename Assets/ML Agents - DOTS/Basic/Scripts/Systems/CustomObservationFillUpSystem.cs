using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Zelcam4.MLAgents;

[BurstCompile]
[UpdateBefore(typeof(ObservationCollectionGroup))]
public partial struct CustomObservationFillUpSystem  : ISystem
{
    
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new UpdateCustomObservationPositionJob
        {
            TransformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true)
        };
            
        state.Dependency = job.ScheduleParallel(state.Dependency);
    }
}
[BurstCompile]
public partial struct UpdateCustomObservationPositionJob : IJobEntity
{
    [ReadOnly]
    public ComponentLookup<LocalTransform> TransformLookup;

    private void Execute(ref CustomObservation observation)
    {
        var targetEntity = observation.Target;


        if (targetEntity != Entity.Null && TransformLookup.HasComponent(targetEntity))
        {

            observation.Position = TransformLookup[targetEntity].Position;
        }
        else observation.Position = float3.zero;
    }
}
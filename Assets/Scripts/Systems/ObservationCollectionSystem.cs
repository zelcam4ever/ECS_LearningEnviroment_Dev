using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace EcsTraining
{ 
public partial class ObservationCollectionSystem : SystemBase
{
    public NativeStream PendingStream;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (PendingStream.IsCreated)
        {
            PendingStream.Dispose();
        }
    }
    
    protected override void OnUpdate()
    {
        
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        
        EntityQuery observationDataQuery = GetEntityQuery(typeof(Agent)); //Change to ObservationInfo or smth
        
        if (PendingStream.IsCreated)
        {
            PendingStream.Dispose();
        }
        
        var chunkCount = observationDataQuery.CalculateChunkCount();
        PendingStream = new NativeStream(chunkCount, Allocator.TempJob);
        
        /*var writeJob = new CollectObservationsJob
        {
            ObservationStreamWriter = ObservationStream.AsWriter(),
        }.ScheduleParallel(chunkCount, 32, state.Dependency);*/

        /*Dependency = new DamagersWriteToStreamJob
        {
            EntityType = GetEntityTypeHandle(),
            DamagerType = GetComponentTypeHandle<Damager>(true),
            StreamDamageEvents = PendingStream.AsWriter(),
        }.ScheduleParallel(damagersQuery, Dependency);

        Dependency = new ParallelApplyStreamEventsToEntitiesJob
        {
            StreamDamageEvents = PendingStream.AsReader(),
            StorageInfoFromEntity = GetEntityStorageInfoLookup(),
            HealthType = GetComponentTypeHandle<Health>(false),
        }.Schedule(chunkCount, 8, Dependency);*/
        
        //writeJob.Complete();
    }

    [BurstCompile]
    public struct CollectObservationsJob : IJobParallelFor
    {
        public NativeStream.Writer ObservationStreamWriter;

        public void Execute(int index)
        {
            ObservationStreamWriter.BeginForEachIndex(index);

            ObservationStreamWriter.Write(new float3(1.0f, 0.5f, -1.2f)); // Example observation
            ObservationStreamWriter.Write(0.0f); // Example reward

            ObservationStreamWriter.EndForEachIndex();
        }
    }
}
    
}
using System.Collections.Generic;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    [UpdateAfter(typeof(IncrementStepSystem))]
    public partial struct ObservationCollectionSystem : ISystem
    {
        private EntityQuery _AgentQuery;
        private ComponentLookup<LocalTransform> _TransformLookup;
        private ComponentLookup<AgentEcs> _AgentsLookup;
        
        private BufferTypeHandle<ObservationSource> _SourceBufferTypeHandle;
        private  BufferTypeHandle<ObservationValue> _ValueBufferTypeHandle;
        
        private EntityTypeHandle _entityTypeHandle;
        

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // Define the query for entities that need observations gathered.
            _AgentQuery = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<ObservationSource, ObservationValue, AgentEcs>()
                .Build(ref state);
            
            // We require these components to exist for the job, so we get their lookups.
            // The 'true' argument means we will only read from them.
            _TransformLookup = state.GetComponentLookup<LocalTransform>(true);
            _AgentsLookup = state.GetComponentLookup<AgentEcs>(true);
            _entityTypeHandle = GetEntityTypeHandle();

            _SourceBufferTypeHandle = state.GetBufferTypeHandle<ObservationSource>(true); // ReadOnly
            _ValueBufferTypeHandle = state.GetBufferTypeHandle<ObservationValue>(false); // Read/Write
        
            // Don't run the system if no agents exist.
            state.RequireForUpdate(_AgentQuery);
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Important: ComponentLookups must be updated every frame.
            // This syncs them with the latest state of the world.
            _TransformLookup.Update(ref state);
            _AgentsLookup.Update(ref state);
            _entityTypeHandle.Update(ref state);
            _SourceBufferTypeHandle.Update(ref state);
            _ValueBufferTypeHandle.Update(ref state);

            var job = new GatherObservationsJob
            {
                EntityTypeHandle = _entityTypeHandle,
                // Get the handles for the buffer types. These tell the job how to access the data in a chunk.
                SourceBufferTypeHandle = _SourceBufferTypeHandle, 
                ValueBufferTypeHandle = _ValueBufferTypeHandle, 
            
                // Pass the updated lookups to the job.
                TransformLookup = _TransformLookup,
                AgentsLookup = _AgentsLookup,
            };

            // Schedule the job to run in parallel on all chunks matching our query.
            state.Dependency = job.ScheduleParallel(_AgentQuery, state.Dependency);
        }
        
        [BurstCompile]
        private struct GatherObservationsJob : IJobChunk
        {
            // Added handle to get entities
            public EntityTypeHandle EntityTypeHandle; 
            
            [ReadOnly] public BufferTypeHandle<ObservationSource> SourceBufferTypeHandle;
            public BufferTypeHandle<ObservationValue> ValueBufferTypeHandle;

            [ReadOnly] public ComponentLookup<LocalTransform> TransformLookup;
            [ReadOnly] public ComponentLookup<AgentEcs> AgentsLookup;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                // Get a NativeArray of all entities in the chunk first
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityTypeHandle);

                BufferAccessor<ObservationSource> sourceAccessor = chunk.GetBufferAccessor(ref SourceBufferTypeHandle);
                BufferAccessor<ObservationValue> valueAccessor = chunk.GetBufferAccessor(ref ValueBufferTypeHandle);

                for (int i = 0; i < chunk.Count; i++)
                {
                    // Get the current entity from the array
                    var entity = entities[i];
                    
                    DynamicBuffer<ObservationSource> sources = sourceAccessor[i];
                    DynamicBuffer<ObservationValue> values = valueAccessor[i];

                    for (int j = 0; j < sources.Length; j++)
                    {
                        ObservationSource source = sources[j];
                        float observation = 0f;

                        switch (source.SourceType)
                        {
                            case ObservationSourceType.PositionX:
                                if (TransformLookup.HasComponent(entity))
                                    observation = TransformLookup[entity].Position.x;
                                break;
                            case ObservationSourceType.PositionY:
                                if (TransformLookup.HasComponent(entity))
                                    observation = TransformLookup[entity].Position.y;
                                break;
                            case ObservationSourceType.PositionZ:
                                if (TransformLookup.HasComponent(entity))
                                    observation = TransformLookup[entity].Position.z;
                                break;

                            case ObservationSourceType.PositionXTarget:
                                if (AgentsLookup.HasComponent(entity))
                                    observation = TransformLookup[AgentsLookup[entity].Target].Position.x;
                                break;
                            case ObservationSourceType.PositionYTarget:
                                if (AgentsLookup.HasComponent(entity))
                                    observation = TransformLookup[AgentsLookup[entity].Target].Position.y;
                                break;
                            case ObservationSourceType.PositionZTarget:
                                if (AgentsLookup.HasComponent(entity))
                                    observation = TransformLookup[AgentsLookup[entity].Target].Position.z;
                                break;
                        }
                        
                        values[j] = observation;
                    }
                }

                entities.Dispose();
            }
        }
    }
}
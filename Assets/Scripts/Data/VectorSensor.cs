using Unity.Collections;
using Unity.Entities;

namespace EcsTraining
{
    public struct VectorSensor : IComponentData
    {
        public NativeArray<float> Observation;
        public ObservationSpec ObservationSpec;
        
    }

    //WIP: Implement to configure ObservationSpace
    public struct ObservationSpec : IComponentData
    {
        
    }
}

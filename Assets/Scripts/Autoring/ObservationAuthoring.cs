using Unity.Entities;
using UnityEngine;

namespace EcsTraining
{
    public class ObservationAuthoring : MonoBehaviour
    {
        public Transform[] elementLocations;
        private class Baker : Baker<ObservationAuthoring>
        {
            public override void Bake(ObservationAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var buffer = AddBuffer<ObservationElementData>(entity);
            
                foreach (var observationElement in authoring.elementLocations)
                {
                    buffer.Add(new ObservationElementData() { Value = observationElement.position.x });
                    buffer.Add(new ObservationElementData() { Value = observationElement.position.y });
                    buffer.Add(new ObservationElementData() { Value = observationElement.position.z });
                }
            }
        }
    }
}
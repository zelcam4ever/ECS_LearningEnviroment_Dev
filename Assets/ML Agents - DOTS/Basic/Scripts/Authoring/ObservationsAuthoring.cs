using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using System.Collections.Generic;
using Zelcam4.MLAgents;

public class ObservationsAuthoring : MonoBehaviour
{
    [SerializeField]private Transform Target;
    public List<TransformConfig> TransformObservations;
    public List<CustomObservationConfig> CustomObservations;
    
    private class Baker : Baker<ObservationsAuthoring>
    {
        public override void Bake(ObservationsAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            int currentIndex = 0; 
            
            // Repeat for each configuration of observations 
            if (authoring.TransformObservations.Count > 0)
            {
                var requestBuffer = AddBuffer<ObservationRequest<LocalTransform>>(entity);
                foreach (var config in authoring.TransformObservations)
                {
                    requestBuffer.Add(new ObservationRequest<LocalTransform>
                    {
                        SourceSubId = (byte)config.SourceType,
                        TargetIndex = currentIndex
                    });
                    currentIndex++; // Increment the index for the next observation
                }
            }
            
            // Configuration for custom observation
            AddComponent(entity, new CustomObservation() 
            {
                Target = GetEntity(authoring.Target, TransformUsageFlags.Dynamic),
            });
            
            //Authoring for custom observations
            if (authoring.CustomObservations.Count > 0)
            {
                var requestBuffer = AddBuffer<ObservationRequest<CustomObservation>>(entity);
                foreach (var config in authoring.CustomObservations)
                {
                    requestBuffer.Add(new ObservationRequest<CustomObservation>
                    {
                        SourceSubId = (byte)config.SourceType,
                        TargetIndex = currentIndex
                    });
                    currentIndex++; // Increment the index for the next observation
                }
            }
            
            // Values to send buffer
            var valueBuffer = AddBuffer<ObservationValue>(entity);
            if (currentIndex > 0)
            {
                valueBuffer.ResizeUninitialized(currentIndex);
            }
        }
    }
}
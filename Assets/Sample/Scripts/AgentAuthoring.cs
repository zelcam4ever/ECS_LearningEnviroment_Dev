using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using System.Collections.Generic;
using Zelcam4.MLAgents;

// Register all the generic component types this authoring script can create.


public class AgentAuthoring : MonoBehaviour
{
    [Header("Observations")]
    // Expose lists for each observation type in the Inspector.
    public List<TransformConfig> TransformObservations;
    
    public class AgentObserverBaker : Baker<AgentAuthoring>
    {
        public override void Bake(AgentAuthoring authoring)
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
            
            // Values to send buffer
            var valueBuffer = AddBuffer<ObservationValue>(entity);
            if (currentIndex > 0)
            {
                valueBuffer.ResizeUninitialized(currentIndex);
            }
        }
    }
}
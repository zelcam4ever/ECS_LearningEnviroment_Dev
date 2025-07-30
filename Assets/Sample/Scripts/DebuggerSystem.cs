using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Zelcam4.MLAgents; // Required for Debug.Log

// Make sure this runs after your observation collection system.
public partial struct DebugObservationSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // SystemAPI.Query(...).ForEach(...) is the standard way to loop over
        // entities on the main thread.
        foreach (var observations in
                 SystemAPI.Query<DynamicBuffer<ObservationValue>>())
        {
            // Safety check to make sure the buffer has enough data.
            if (observations.Length < 3)
            {
                continue;
            }

            // Read the position from the first three values in the buffer.
            var agentPos = new float3(observations[0].Value, observations[1].Value, observations[2].Value);

            // Log the position to the Unity console.
            Debug.Log($"Agent Position from Buffer: {agentPos}");
        }
    }
}
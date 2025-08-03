using Unity.Transforms;
using Unity.Entities;
using Unity.Physics;
using Zelcam4.MLAgents;

// Use the 'assembly:' keyword to apply this attribute to the entire assembly.
// This tells the DOTS compiler to be aware of this specific generic component type.
[assembly: RegisterGenericComponentType(typeof(ObservationRequest<LocalTransform>))]
[assembly: RegisterGenericComponentType(typeof(ObservationRequest<CustomObservation>))]

// If you add more observation types in the future, add them here too.
// For example:
// [assembly: RegisterGenericComponentType(typeof(ObservationRequest<PhysicsVelocity>))]
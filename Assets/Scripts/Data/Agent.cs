using Unity.Collections;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace EcsTraining
{
    public struct AgentReset : IComponentData, IEnableableComponent {}
    
    public struct OnEpisodeBegin : IComponentData, IEnableableComponent {}
}
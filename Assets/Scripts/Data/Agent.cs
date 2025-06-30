using Unity.Collections;
using Unity.Entities;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace EcsTraining
{
    public struct Agent : IComponentData
    {
        //Reference to brain, maybe id?
        //BehaviourParameters (PolicyFactory)
        //public AgentInfo InfoBrain;
        public float Reward;
        public float GroupReward;
        public float CumulativeReward;
        public bool RequestAction;   //TODO: should be IEnableComponent?
        public bool RequestDecision; //TODO: should be IEnableComponent?
        public int MaxStep;
        public int StepCount;
        public int CompletedEpisodes;
        public int EpisodeId;
        public bool Initialized;
        //List<ISensor> sensors;
        //StackingSensor
        //ActuatorManager / List<IActuator>
        public int GroupId;
        public bool IsEnabled;      //TODO: should be IEnableComponent?

        //Adding From AgentInfo
        public bool MaxStepReached;
        public bool Done;
        
        public Entity Target;
        public Entity GoundRender;
    }

    public struct AgentReset : IComponentData, IEnableableComponent {}
    
    public struct OnEpisodeBegin : IComponentData, IEnableableComponent {}
}
using Unity.Entities;

namespace Zelcam4.MLAgents
{
    public struct ObservationValue : IBufferElementData
    {
        public float Value;
        
        // Convenience
        public static implicit operator float(ObservationValue e) { return e.Value; }
        public static implicit operator ObservationValue(float f) { return new ObservationValue { Value = f }; }
    }
    public struct AgentEcs : IComponentData
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
        public bool StartingEpisode;

        public Entity Target;
        public Entity GoundRender;
    }
}

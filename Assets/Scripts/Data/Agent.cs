using Unity.Collections;
using Unity.Entities;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace EcsTraining
{
    /// <summary>
    /// Stores the global data, episodes, steps and areas.
    /// </summary>
    public struct AgentInfo: IComponentData
    {
        //public ActionBuffers StoredActions;
        public NativeArray<bool> DiscreteActionMasks;
        public float Reward;
        public float GroupReward;
        public bool Done;
        public bool MaxStepReached;
        public int EpisodeId;
        public int GroupId;
        
        /*public void ClearActions()
        {
            StoredActions.Clear();
        }*/
    }
    
    public struct Agent : IComponentData
    {
        //Reference to brain, maybe id?
        //BehaviourParameters (PolicyFactory)
        public AgentInfo InfoBrain;
        public float Reward;
        public float GroupReward;
        public float CumulativeReward;
        public bool RequestAction;   //TODO: should be IEnableComponent?
        public bool RequestDecision; //TODO: should be IEnableComponent?
        public int StepCount;
        public int CompletedEpisodes;
        public int EpisodeId;
        public bool Initialized;
        //List<ISensor> sensors;
        //StackingSensor
        //ActuatorManager / List<IActuator>
        public int GroupId;
        public bool IsEnabled;      //TODO: should be IEnableComponent?
    }
}
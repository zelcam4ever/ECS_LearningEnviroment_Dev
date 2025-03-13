using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class AgentOopDebug : Agent
{
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.DiscreteActions[0]);
    }
}

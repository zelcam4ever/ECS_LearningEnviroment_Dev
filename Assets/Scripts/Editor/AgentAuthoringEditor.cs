using EcsTraining;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentAutoring))]
[CanEditMultipleObjects]
public class AgentAuthoringEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AgentAutoring agentAuthoring = (AgentAutoring)target;
        var serializedAgent = serializedObject;
        serializedAgent.Update();

        DrawPropertiesExcluding(serializedObject, "decisionRequester", "decisionPeriod", "decisionStep", "takeActionsBetweenDecisions");
        
        // Allows us to add and configure a Decision Requester within the agent component
        agentAuthoring.decisionRequester = EditorGUILayout.ToggleLeft("Decision Requester", agentAuthoring.decisionRequester, EditorStyles.boldLabel);
        
        if (agentAuthoring.decisionRequester)
        {
            agentAuthoring.decisionPeriod = EditorGUILayout.IntSlider("Decision Period",agentAuthoring.decisionPeriod, 1,20);
            agentAuthoring.decisionStep = EditorGUILayout.IntSlider("Decision Step",agentAuthoring.decisionStep, 0,agentAuthoring.decisionPeriod - 1);
            agentAuthoring.takeActionsBetweenDecisions = EditorGUILayout.Toggle("Take Actions Between Decisions",agentAuthoring.takeActionsBetweenDecisions);
        }
        serializedAgent.ApplyModifiedProperties();
    }
}

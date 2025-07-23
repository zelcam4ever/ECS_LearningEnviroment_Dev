using EcsTraining;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentAutoring))]
public class AgentAuthoringEditor : Editor
{
    // This method overrides the default way the Inspector is drawn
    public override void OnInspectorGUI()
    {
        AgentAutoring agentAutoring = (AgentAutoring)target;
        DrawPropertiesExcluding(serializedObject, "decisionRequester", "decisionPeriod", "");
        
        agentAutoring.decisionRequester = EditorGUILayout.ToggleLeft("Decision Requester", agentAutoring.decisionRequester, EditorStyles.boldLabel);
        
        if (agentAutoring.decisionRequester)
        {
            agentAutoring.decisionPeriod = EditorGUILayout.IntSlider("Decision Period",agentAutoring.decisionPeriod, 1,20);
            agentAutoring.decisionStep = EditorGUILayout.IntSlider("Decision Step",agentAutoring.decisionStep, 0,19);
            agentAutoring.takeActionsBetweenDecisions = EditorGUILayout.Toggle("Take Actions Between Decisions",agentAutoring.takeActionsBetweenDecisions);
        }
    }
}

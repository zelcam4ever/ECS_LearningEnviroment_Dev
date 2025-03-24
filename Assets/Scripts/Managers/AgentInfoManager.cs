using System;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace EcsTraining
{ 
public static class AgentInfoManager
{
    private static Dictionary<int, AgentInfo> _agentInfos = new Dictionary<int, AgentInfo>();
    private static int _currentId;
    
    public static void CreateAgentInfo()
    {
        var agentInfo = new AgentInfo();
        agentInfo.storedActions = new ActionBuffers(Array.Empty<float>(),
            new int[1]
        );
        _agentInfos.Add(_currentId, agentInfo);
        _currentId++;
    }
    
    public static AgentInfo GetAgentInfo(int id)
    {
        return _agentInfos[id];
    }

    public static void SetAgentInfo(int id, AgentInfo info)
    {
        _agentInfos[id] = info;
    }
    
    
    public static void UpdateAgentInfo(int id, Action<AgentInfo> updateAction)
    {
        if (_agentInfos.TryGetValue(id, out var agentInfo))
        {
            updateAction(agentInfo); // Modify the local copy
            _agentInfos[id] = agentInfo;  // Assign the modified struct back
        }
    }
}
}
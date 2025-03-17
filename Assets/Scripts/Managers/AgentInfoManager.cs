using System;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace EcsTraining
{ 
public static class AgentInfoManager
{
    private static Dictionary<int, AgentInfo> _agentInfos = new Dictionary<int, AgentInfo>();
    private static int _currentId;
    
    public static int CreateAgentInfo()
    {
        _currentId++;
        var agentInfo = new AgentInfo();
        _agentInfos.Add(_currentId, agentInfo);
        return _currentId;
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
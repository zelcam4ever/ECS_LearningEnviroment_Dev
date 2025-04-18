using System;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace EcsTraining
{ 
public static class AgentInfoManager
{
    private static Dictionary<int, AgentInfo> _agentInfos = new Dictionary<int, AgentInfo>();
    
    
    /*public static void CreateAgentInfo(int id)
    {
        var agentInfo = new AgentInfo();
        agentInfo.storedActions = new ActionBuffers(Array.Empty<float>(),
            new int[3]
        );
        _agentInfos.Add(id, agentInfo);
    }*/
    
    public static AgentInfo GetAgentInfo(int id)
    {
        if (!_agentInfos.ContainsKey(id))
        {
            Debug.Log("Created new agent info: " + id);
            var agentInfo = new AgentInfo();
            agentInfo.storedActions = new ActionBuffers(Array.Empty<float>(),
                new int[3]
            );
            _agentInfos.Add(id, agentInfo);
        }
        
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
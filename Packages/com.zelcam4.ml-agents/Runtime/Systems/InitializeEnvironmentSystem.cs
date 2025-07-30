using Unity.Burst;
using Unity.Entities;
using static Unity.Entities.SystemAPI;
using Zelcam4.MLAgents;
using UnityEngine;

namespace Zelcam4.MLAgents
{
    [UpdateBefore(typeof(RequesterSystem))]
    public partial struct InitializeEnvironmentSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            CommunicatorManager.AwakeCalled();
        }
    }
}
    


using Unity.Burst;
using Unity.Entities;
using static Unity.Entities.SystemAPI;
using Unity.MLAgents;
using UnityEngine;

namespace EcsTraining
{
    [UpdateBefore(typeof(IncrementStepSystem))]
    public partial struct InitializeEnvironmentSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Training>();
            Debug.Log("We cooking?");
            CommunicatorManager.AwakeCalled();
            Debug.Log("Hola initialized");
            AgentInfoManager.CreateAgentInfo();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }
        /*protected override void OnUpdate()        {
            
            var config = SystemAPI.GetSingleton<Training>();
            
            /*var academy = SystemAPI.GetSingletonRW<Training>().ValueRW;

            if (academy.IsInitialized)return;
            academy.IsInitialized = true;#1#
            
            /*if (!CommunicatorFactory.CommunicatorRegistered)
            {
                Debug.Log("Registered Communicator in Academy.");
                //CommunicatorFactory.Register<ICommunicator>(RpcCommunicator.Create);
            }#1#

        }*/

    }
}
    


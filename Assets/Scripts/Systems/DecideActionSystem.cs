using Unity.Entities;
using static Unity.Entities.SystemAPI;

namespace EcsTraining
{
    public partial struct DecideActionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AcademyTraining>(); //?
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var agent in Query<RefRW<Agent>>())
            {
                //if (ActuatorManager.StoredActions.ContinuousActions.Array == null) ResetData();
                //var actions = Brain.DecideAction(); TODO: This is hella tricky
                
                //agent.ValueRW.InfoBrain.CopyAction(actions);
                
                //m_ActuatorManager.UpdateActions(actions);
            }
        }
    }
}
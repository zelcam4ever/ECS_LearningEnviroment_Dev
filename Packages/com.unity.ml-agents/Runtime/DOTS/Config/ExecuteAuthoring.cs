using Unity.Entities;
using UnityEngine;

namespace Zelcam4.MLAgents.DOTS
{
    class ExecuteAuthoring : MonoBehaviour
    {
        [Header("First config bool")] public bool Training;
    }
    
    class ExecuteAuthoringBaker : Baker<ExecuteAuthoring>
    {
        public override void Bake(ExecuteAuthoring authoring)
        {
            var entity  = GetEntity(TransformUsageFlags.None);
            if(authoring.Training) AddComponent<Training>(entity);
        }
    }
    
    public struct Training : IComponentData {}
}
using Unity.Entities;
using UnityEngine;

namespace EcsTraining
{
    public class AcademyAuthoring: MonoBehaviour
    {
        private class Baker : Baker<AcademyAuthoring>
        {
            public override void Bake(AcademyAuthoring authoring)
            {
            
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new AcademyTraining());
            }
        }
    }
}
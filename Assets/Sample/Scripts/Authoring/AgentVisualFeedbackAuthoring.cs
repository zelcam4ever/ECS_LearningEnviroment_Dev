using Unity.Entities;
using UnityEngine;

namespace Sample.Scripts
{
    public class AgentVisualFeedbackAuthoring: MonoBehaviour
    {
        [Tooltip("The Transform of the GameObject whose material should be changed on episode completion.")]
        public Transform RendererTransform;

        public class Baker : Baker<AgentVisualFeedbackAuthoring>
        {
            public override void Bake(AgentVisualFeedbackAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
            
                AddComponent(entity, new AgentVisualFeedback
                {
                    // Convert the Transform reference to an Entity and store it.
                    RendererEntity = GetEntity(authoring.RendererTransform, TransformUsageFlags.None)
                });
            }
        }
}}
using Unity.Entities;

namespace Sample.Scripts
{
    public struct AgentVisualFeedback : IComponentData
    {
        public Entity RendererEntity;
        public int MaterialIndex;
    }
}
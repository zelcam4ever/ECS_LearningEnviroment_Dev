using Unity.Collections;
using Unity.Entities;

namespace MLAgents.DOTS
{
    public struct CommunicatorInformation : IComponentData
    {
        public NativeList<int> BrainKeys;
    }
    public struct CommunicatorConfig: IComponentData
    {
        public static readonly FixedString64Bytes ApiVersion = "1.5.0";
        public static readonly FixedString64Bytes PackageVersion = "3.0.0";
        public static readonly int EditorTrainingPort = 5004;
        public static readonly FixedString64Bytes PortCommandLineFlag = "--mlagents-ecs-port";
        public static int InferenceSeed;
    }
}


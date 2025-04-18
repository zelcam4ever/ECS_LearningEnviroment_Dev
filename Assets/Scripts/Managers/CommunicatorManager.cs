using System;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace EcsTraining
{ 
public static class CommunicatorManager 
{
    /// Pointer to the communicator currently in use by the Academy.
    internal static ICommunicator Communicator;
    
    static bool m_Initialized;
    
    /// <summary>
    /// Unity package version of com.unity.ml-agents.
    /// This must match the version string in package.json and is checked in a unit test.
    /// </summary>
    internal const string k_PackageVersion = "3.0.0";

    const int k_EditorTrainingPort = 5004;

    const string k_PortCommandLineFlag = "--mlagents-port";
    
    const string k_ApiVersion = "1.5.0";
    
    // Random seed used for inference.
    static int m_InferenceSeed;

    /// <summary>
    /// Set the random seed used for inference. This should be set before any Agents are added
    /// to the scene. The seed is passed to the ModelRunner constructor, and incremented each
    /// time a new ModelRunner is created.
    /// </summary>
    public static int InferenceSeed
    {
        set { m_InferenceSeed = value; }
    }
    
    static int m_NumAreas;

    /// <summary>
    /// Number of training areas to instantiate.
    /// </summary>
    public static int NumAreas => m_NumAreas;
    
    /// <summary>
    /// Returns the RLCapabilities of the python client that the unity process is connected to.
    /// </summary>
    internal static UnityRLCapabilities TrainerCapabilities { get; set; }
    
    public static void AwakeCalled()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (!CommunicatorFactory.CommunicatorRegistered)
        {
            Debug.Log("Registered Communicator in Agent.");
            CommunicatorFactory.Register<ICommunicator>(RpcCommunicator.Create);
        }
#endif

        LazyInitialize();
    }
    
    // Used to read Python-provided environment parameters
    static int ReadPortFromArgs()
    {
        var args = Environment.GetCommandLineArgs();
        var inputPort = "";
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i] == k_PortCommandLineFlag)
            {
                inputPort = args[i + 1];
            }
        }

        try
        {
            Debug.Log("ReadPortFromArgs working");
            return int.Parse(inputPort);
        }
        catch
        {
            // No arg passed, or malformed port number.
#if UNITY_EDITOR
            // Try connecting on the default editor port
            //TODO:return MLAgentsSettingsManager.Settings.ConnectTrainer ? MLAgentsSettingsManager.Settings.EditorPort : -1;
            Debug.Log("No bueno");
            return -1;
#else
                // This is an executable, so we don't try to connect.
                return -1;
#endif
        }
    }

    internal static void LazyInitialize()
    {
        if (!m_Initialized)
        {
            InitializingAcademy();
            m_Initialized = true;
        }
    }
    /// <summary>
    /// Initializes the environment, configures it and initializes the Academy.
    /// </summary>
    static void InitializingAcademy()
    {
        //var port = ReadPortFromArgs();
        var port = 5004;
        if (port > 0)
        {
            Communicator = CommunicatorFactory.Create();
        }

        if (Communicator == null && CommunicatorFactory.Enabled && port > 0)
        {
            Debug.Log("Communicator failed to start!");
        }

        // We try to exchange the first message with Python. If this fails, it means
        // no Python Process is ready to train the environment. In this case, the
        // environment must use Inference.
        bool initSuccessful = false;
        var communicatorInitParams = new CommunicatorInitParameters
        {
            port = port,
            unityCommunicationVersion = k_ApiVersion,
            unityPackageVersion = k_PackageVersion,
            name = "AcademySingleton",
            CSharpCapabilities = new UnityRLCapabilities()
        };

        try
        {
            initSuccessful = Communicator.Initialize(
                communicatorInitParams,
                out var unityRlInitParameters
            );
            if (initSuccessful)
            {
                UnityEngine.Random.InitState(unityRlInitParameters.seed);
                // We might have inference-only Agents, so set the seed for them too.
                m_InferenceSeed = unityRlInitParameters.seed;
                m_NumAreas = unityRlInitParameters.numAreas;
                TrainerCapabilities = unityRlInitParameters.TrainerCapabilities;
                TrainerCapabilities.WarnOnPythonMissingBaseRLCapabilities();
                Debug.Log("We are connected holy shit");
            }
            else
            {
                Debug.Log($"Couldn't connect to trainer on port {port} using API version {k_ApiVersion}. Will perform inference instead.");
                Communicator = null; 
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Unexpected exception when trying to initialize communication: {ex}\nWill perform inference instead.");
            Communicator = null;
        }
    }

    //RemoteCommunicucator: Change
    public static void PutObservation(string behaviorName, AgentInfo info, List<ISensor> sensors)
    {
        Communicator.PutObservations(behaviorName, info, sensors);
    }

    public static void SubscribeBrain(string name, ActionSpec actionSpec)
    {
        Communicator.SubscribeBrain(name, actionSpec);
    }

    public static ActionBuffers DecideAction(string brainname, int agentId)
    {
        Communicator.DecideBatch();
        var actions = Communicator?.GetActions(brainname, agentId);
        //TODO: return ref m_LastActionBuffer = actions == null ? ActionBuffers.Empty : (ActionBuffers)actions;
        return actions ?? ActionBuffers.Empty;
    }
}
}


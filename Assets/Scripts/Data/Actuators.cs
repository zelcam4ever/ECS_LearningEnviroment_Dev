using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.MLAgents.Actuators;
using UnityEngine;

namespace EcsTraining
{
    /*/// <summary>
    /// Stores the global data, episodes, steps and areas.
    /// </summary>
    public struct AgentInfo
    {
        //public ActionBuffers StoredActions;
        public ActionBuffers Miim;
        //public NativeArray<bool> DiscreteActionMasks;
        public float Reward;
        public float GroupReward;
        public bool Done;
        public bool MaxStepReached;
        public int EpisodeId;
        public int GroupId;
        
        /*public void ClearActions()
        {
            StoredActions.Clear();
        }#1#
        
        /*public void CopyActions()
        {
            var continuousActions = storedActions.ContinuousActions;
            for (var i = 0; i < actionBuffers.ContinuousActions.Length; i++)
            {
                continuousActions[i] = actionBuffers.ContinuousActions[i];
            }
            var discreteActions = storedActions.DiscreteActions;
            for (var i = 0; i < actionBuffers.DiscreteActions.Length; i++)
            {
                discreteActions[i] = actionBuffers.DiscreteActions[i];
            }
        }#1#
    }

    public struct ActionBuffers : DynamicBuffer<ActionSegment>
    {
        /// <summary>
        /// Holds the Continuous <see cref="ActionSegment{T}"/> to be used by an <see cref="IActionReceiver"/>.
        /// </summary>
        public DynamicBuffer<float> ContinuousActions { get; }
        
        /// <summary>
        /// Holds the Discrete <see cref="ActionSegment{T}"/> to be used by an <see cref="IActionReceiver"/>.
        /// </summary>
        public DynamicBuffer<int> DiscreteActions { get; }
        
        
        
    }

    public struct ActionSegment : IBufferElementData
    {
        
    }*/
    
    //WIP: Implement to configure ActionSpace
    /*public struct ActionSpec : IComponentData
    {
        public int NumContinuousActions;
        public NativeArray<int> BranchSizes;
        
        /// <summary>
        /// The number of branches for discrete actions that an Agent can take.
        /// </summary>
        public int NumDiscreteActions => BranchSizes == null ? 0 : BranchSizes.Length;
        
        /// <summary>
        /// Get the total number of Discrete Actions that can be taken by calculating the Sum
        /// of all the Discrete Action branch sizes.
        /// </summary>
        public int SumOfDiscreteBranchSizes => BranchSizes == null ? 0 : BranchSizes.Sum();
    }*/
}
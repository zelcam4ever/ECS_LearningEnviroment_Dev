using UnityEngine;

namespace Zelcam4.MLAgents.Editor
{
    /// <summary>
    /// A static helper class for the Editor components of the ML-Agents SDK.
    /// </summary>
    public static class EditorUtilities
    {
        /// <summary>
        /// Whether or not properties that affect the model can be updated at the current time.
        /// </summary>
        /// <returns>True if the model can be updated, False if not.</returns>
        public static bool CanUpdateModelProperties()
        {
            return !Application.isPlaying;
        }
    }
}

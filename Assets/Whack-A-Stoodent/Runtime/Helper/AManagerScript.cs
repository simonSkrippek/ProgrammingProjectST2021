using UnityEngine;

namespace WhackAStoodent.Helper
{
    /// <summary>
    /// An abstract class that describes a manager script
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AManagerScript<T> : MonoBehaviour where T : AManagerScript<T>
    {
        /// <summary>
        /// Manager instance
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// Called when behaviour has been enabled
        /// </summary>
        protected virtual void OnEnable()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
        }

        /// <summary>
        /// Called when behavior has been disabled
        /// </summary>
        protected virtual void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}

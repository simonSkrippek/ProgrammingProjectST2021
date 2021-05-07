using UnityEngine;

namespace WhackAStoodent.Runtime.Helper
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}

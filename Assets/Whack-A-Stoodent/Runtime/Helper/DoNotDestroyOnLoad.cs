using UnityEngine;

namespace WhackAStoodent.Helper
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}

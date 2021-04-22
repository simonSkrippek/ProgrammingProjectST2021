using UnityEngine;

namespace Electrodz.Runtime.Scripts
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}

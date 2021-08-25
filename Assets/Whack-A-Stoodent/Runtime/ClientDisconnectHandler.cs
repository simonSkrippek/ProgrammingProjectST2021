using UnityEngine;
using WhackAStoodent.Client;

namespace WhackAStoodent
{
    public class ClientDisconnectHandler : MonoBehaviour
    {
        [SerializeField] private SceneManager sceneManager;
        
        private void Awake()
        {
            ClientManager.Instance.ConnectionInterrupted += HandleClientDisconnect;
        }
        private void OnDestroy()
        {
            ClientManager.Instance.ConnectionInterrupted -= HandleClientDisconnect;
        }

        private void HandleClientDisconnect()
        {
            Debug.Log("Connection to the server was lost: Reloading to reconnect");
            sceneManager.LoadScene(0);
        }
    }
}
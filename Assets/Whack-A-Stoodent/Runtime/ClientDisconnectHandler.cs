using System;
using UnityEngine;
using WhackAStoodent.Runtime.Client;

namespace WhackAStoodent.Runtime
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
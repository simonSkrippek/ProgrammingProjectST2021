using UnityEngine;

namespace WhackAStoodent.Runtime.Client.Networking.Connectors
{
    public class NetworkConnectionSettings : AConnectionSettings
    {
        [SerializeField, Tooltip("The IP address of the server to connect to")] public string ipAddress;
        [SerializeField, Tooltip("The port on which to address the server")] public ushort port;
        [SerializeField, Tooltip("The time that is waited for a packet in the packet-receiving-loop if no packet is already present (in milliseconds). recommended is a low value != 0")] public ushort timeoutTime;
    }
}
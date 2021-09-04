using System;
using UnityEngine;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Client.Networking.Connectors
{
    [CreateAssetMenu(order = 1, fileName = "NetworkConnectionSettings", menuName = "ConnectionSettings/Network")]
    public class NetworkConnectionSettings : AConnectionSettings
    {
        public string IpAddress =>
            (StorageUtility.TryLoadIPAddress(out string loaded_ip) && !string.IsNullOrEmpty(loaded_ip))
                ? loaded_ip
                : ipAddress; 
        [SerializeField, Tooltip("The IP address of the server to connect to")] private string ipAddress;
        [SerializeField, Tooltip("The port on which to address the server")] public ushort port;
        [SerializeField, Tooltip("The time that is waited for a packet in the packet-receiving-loop if no packet is already present (in milliseconds). recommended is a low value != 0")] public ushort timeoutTime;
    }
}
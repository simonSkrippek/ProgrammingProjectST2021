using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "AMessageEvent", menuName = "Custom/Events/OneParameterEvents/AMessageEvent", order = 0)]
    public class AMessageEvent : OneParameterEvent<AMessage>
    {
        
    }
}
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "DenialReasonEvent", menuName = "Custom/Events/OneParameterEvents/DenialReasonEvent", order = 0)]
    public class DenialReasonEvent : OneParameterEvent<DenyPlayRequestMessage.EDenialReason>
    {
        
    }
}
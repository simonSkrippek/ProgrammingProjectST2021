using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "MatchHistoryEntryArrayEvent", menuName = "Custom/Events/OneParameterEvents/MatchHistoryEntryArrayEvent", order = 0)]
    public class MatchHistoryEntryArrayEvent : OneParameterEvent<MatchHistoryEntry[]>
    {
        
    }
}
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "MatchDataArrayEvent", menuName = "Custom/Events/OneParameterEvents/MatchDataArrayEvent", order = 0)]
    public class MatchDataArrayEvent : OneParameterEvent<MatchData[]>
    {
        
    }
}
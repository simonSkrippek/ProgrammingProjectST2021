using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "MatchDataEvent", menuName = "Custom/Events/OneParameterEvents/MatchDataEvent", order = 0)]
    public class MatchDataEvent : OneParameterEvent<MatchData>
    {
        
    }
}
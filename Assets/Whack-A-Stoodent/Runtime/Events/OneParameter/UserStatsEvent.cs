using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "UserStatsEvent", menuName = "Custom/Events/OneParameterEvents/UserStatsEvent", order = 0)]
    public class UserStatsEvent : OneParameterEvent<UserStats>
    {
        
    }
}
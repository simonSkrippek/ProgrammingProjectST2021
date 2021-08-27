using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "StringGameRoleEvent", menuName = "Custom/Events/TwoParameterEvents/StringGameRoleEvent", order = 0)]
    public class StringGameRoleEvent : TwoParameterEvent<string, EGameRole>
    {
        
    }
}
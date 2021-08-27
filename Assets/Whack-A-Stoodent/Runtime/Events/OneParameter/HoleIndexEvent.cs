using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "HoleIndexEvent", menuName = "Custom/Events/OneParameterEvents/HoleIndexEvent", order = 0)]
    public class HoleIndexEvent : OneParameterEvent<EHoleIndex>
    {
        
    }
}
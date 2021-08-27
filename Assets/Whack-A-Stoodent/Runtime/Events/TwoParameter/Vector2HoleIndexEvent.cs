using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "Vector2HoleIndexEvent", menuName = "Custom/Events/TwoParameterEvents/Vector2HoleIndexEvent", order = 0)]
    public class Vector2HoleIndexEvent : TwoParameterEvent<Vector2, EHoleIndex>
    {
        
    }
}
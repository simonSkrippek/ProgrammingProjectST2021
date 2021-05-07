using UnityEngine;

namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class HitMessage : AMessage
    {
        public readonly Vector2 _position;
        
        public HitMessage(Vector2 position) : base()
        {
            _position = position;
        }

        public override EMessageType MessageType => EMessageType.Hit;
    }
}
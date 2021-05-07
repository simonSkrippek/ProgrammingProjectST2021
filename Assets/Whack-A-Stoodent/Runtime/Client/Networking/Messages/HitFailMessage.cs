using UnityEngine;

namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class HitFailMessage : AMessage
    {
        public readonly Vector2 _hitPosition;
        
        public HitFailMessage(Vector2 hitPosition) : base()
        {
            _hitPosition = hitPosition;
        }

        public override EMessageType MessageType => EMessageType.HitFail;
    }
}
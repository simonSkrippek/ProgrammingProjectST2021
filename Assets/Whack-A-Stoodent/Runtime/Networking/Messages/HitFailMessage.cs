using UnityEngine;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class HitFailMessage : AMessage
    {
        public readonly Vector2 _hitPosition;
        
        public HitFailMessage(EMessagePurpose messagePurpose, Vector2 hitPosition) : base(messagePurpose)
        {
            _hitPosition = hitPosition;
        }

        public override EMessageType MessageType => EMessageType.HitFail;
    }
}
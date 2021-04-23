using UnityEngine;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class HitSuccessMessage : AMessage
    {
        public readonly EHoleIndex _holeIndex;
        public readonly int _pointsGained;
        public readonly Vector2 _hitPosition;
        
        public HitSuccessMessage(EMessagePurpose messagePurpose, EHoleIndex holeIndex, int pointsGained, Vector2 hitPosition) : base(messagePurpose)
        {
            _holeIndex = holeIndex;
            _pointsGained = pointsGained;
            _hitPosition = hitPosition;
        }

        public override EMessageType MessageType => EMessageType.HitSuccess;
    }
}
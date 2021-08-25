using UnityEngine;

namespace WhackAStoodent.Client.Networking.Messages
{
    public class HitSuccessMessage : AMessage
    {
        public readonly EHoleIndex _holeIndex;
        public readonly long _pointsGained;
        public readonly Vector2 _hitPosition;
        
        public HitSuccessMessage(EHoleIndex holeIndex, long pointsGained, Vector2 hitPosition) : base()
        {
            _holeIndex = holeIndex;
            _pointsGained = pointsGained;
            _hitPosition = hitPosition;
        }

        public override EMessageType MessageType => EMessageType.HitSuccess;
    }
}
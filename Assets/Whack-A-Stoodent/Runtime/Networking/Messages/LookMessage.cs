namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class LookMessage : AMessage
    {
        public readonly EHoleIndex _holeIndex;
        
        public LookMessage(EMessagePurpose messagePurpose, EHoleIndex holeIndex) : base(messagePurpose)
        {
            _holeIndex = holeIndex;
        }

        public override EMessageType MessageType => EMessageType.Look;
    }
}
namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class LookMessage : AMessage
    {
        public readonly EHoleIndex _holeIndex;
        
        public LookMessage(EHoleIndex holeIndex) : base()
        {
            _holeIndex = holeIndex;
        }

        public override EMessageType MessageType => EMessageType.Look;
    }
}
namespace WhackAStoodent.Runtime.Networking.Messages
{
    public abstract class AMessage
    {
        public EMessageType MessageType { get; }

        public EMessagePurpose MessagePurpose { get; }

        public bool IsValid()
        {
            
        }
    }
}
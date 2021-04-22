namespace WhackAStoodent.Runtime.Networking.Messages
{
    public abstract class AMessage
    {
        public abstract EMessageType MessageType { get; }
        public EMessagePurpose MessagePurpose { get; }

        protected AMessage(EMessagePurpose messagePurpose)
        {
            MessagePurpose = messagePurpose;
        }
    }
}
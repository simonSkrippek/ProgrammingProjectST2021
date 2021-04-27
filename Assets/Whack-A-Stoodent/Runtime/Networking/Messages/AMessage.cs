namespace WhackAStoodent.Runtime.Networking.Messages
{
    public abstract class AMessage
    {
        public abstract EMessageType MessageType { get; }

        protected AMessage()
        {
            
        }
    }
}
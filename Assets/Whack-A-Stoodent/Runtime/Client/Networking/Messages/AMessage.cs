namespace WhackAStoodent.Client.Networking.Messages
{
    public abstract class AMessage
    {
        public abstract EMessageType MessageType { get; }

        protected AMessage()
        {
            
        }
    }
}
namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class AcceptPlayRequestMessage : AMessage
    {
        public AcceptPlayRequestMessage() : base()
        {
        }

        public override EMessageType MessageType => EMessageType.AcceptPlayRequest;
    }
}
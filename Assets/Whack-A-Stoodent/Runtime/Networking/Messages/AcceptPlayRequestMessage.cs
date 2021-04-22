namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class AcceptPlayRequestMessage : AMessage
    {
        public AcceptPlayRequestMessage(EMessagePurpose messagePurpose) : base(messagePurpose)
        {
        }

        public override EMessageType MessageType => EMessageType.AcceptPlayRequest;
    }
}
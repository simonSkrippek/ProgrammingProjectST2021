namespace WhackAStoodent.Client.Networking.Messages
{
    public class AcceptPlayRequestMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.AcceptPlayRequest;
    }
}
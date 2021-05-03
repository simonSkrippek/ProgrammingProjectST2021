namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class AcceptPlayRequestMessage : AMessage
    {
        public readonly string _sessionCode;
        
        public AcceptPlayRequestMessage(string sessionCode) : base()
        {
            _sessionCode = sessionCode;
        }

        public override EMessageType MessageType => EMessageType.AcceptPlayRequest;
    }
}
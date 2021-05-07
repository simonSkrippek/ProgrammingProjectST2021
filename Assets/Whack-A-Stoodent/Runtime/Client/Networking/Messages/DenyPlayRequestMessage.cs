namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class DenyPlayRequestMessage : AMessage
    {
        public readonly string _sessionCode;
        public readonly EDenialReason _denialReason;
        
        public DenyPlayRequestMessage(string sessionCode, EDenialReason denialReason) : base()
        {
            _denialReason = denialReason;
            _sessionCode = sessionCode;
        }

        public override EMessageType MessageType => EMessageType.DenyPlayRequest;
        
        public enum EDenialReason
        {
            PlayerChoice,
        }
    }
}
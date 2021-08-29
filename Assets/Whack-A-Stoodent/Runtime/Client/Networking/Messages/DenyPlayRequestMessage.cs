namespace WhackAStoodent.Client.Networking.Messages
{
    public class DenyPlayRequestMessage : AMessage
    {
        public readonly EDenialReason _denialReason;
        
        public DenyPlayRequestMessage(EDenialReason denialReason) : base()
        {
            _denialReason = denialReason;
        }

        public override EMessageType MessageType => EMessageType.DenyPlayRequest;
        
        public enum EDenialReason
        {
            PlayerChoice,
        }
    }
}
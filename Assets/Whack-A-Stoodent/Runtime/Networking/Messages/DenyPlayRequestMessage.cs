namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class DenyPlayRequestMessage : AMessage
    {
        public readonly DenialReason _denialReason;
        
        public DenyPlayRequestMessage(EMessagePurpose messagePurpose, DenialReason denialReason) : base(messagePurpose)
        {
            _denialReason = denialReason;
        }

        public override EMessageType MessageType => EMessageType.DenyPlayRequest;
        
        public enum DenialReason
        {
            PlayerChoice,
        }
    }
}
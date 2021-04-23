namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class HideMessage : AMessage
    {
        public HideMessage(EMessagePurpose messagePurpose) : base(messagePurpose)
        {
        }

        public override EMessageType MessageType => EMessageType.Hide;
    }
}
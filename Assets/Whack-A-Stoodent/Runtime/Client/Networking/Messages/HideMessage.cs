namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class HideMessage : AMessage
    {
        public HideMessage() : base()
        {
        }

        public override EMessageType MessageType => EMessageType.Hide;
    }
}
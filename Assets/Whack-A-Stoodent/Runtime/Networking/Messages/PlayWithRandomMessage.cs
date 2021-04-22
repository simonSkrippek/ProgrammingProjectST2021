namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PlayWithRandomMessage : AMessage
    {
        public PlayWithRandomMessage(EMessagePurpose messagePurpose) : base(messagePurpose)
        {
        }

        public override EMessageType MessageType => EMessageType.PlayWithRandom;
    }
}
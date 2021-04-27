namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PlayWithRandomMessage : AMessage
    {
        public PlayWithRandomMessage() : base()
        {
        }

        public override EMessageType MessageType => EMessageType.PlayWithRandom;
    }
}
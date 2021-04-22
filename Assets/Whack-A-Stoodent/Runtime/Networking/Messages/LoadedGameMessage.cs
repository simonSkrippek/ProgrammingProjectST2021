namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class LoadedGameMessage : AMessage
    {
        public LoadedGameMessage(EMessagePurpose messagePurpose) : base(messagePurpose)
        {
        }

        public override EMessageType MessageType => EMessageType.LoadedGame;
    }
}
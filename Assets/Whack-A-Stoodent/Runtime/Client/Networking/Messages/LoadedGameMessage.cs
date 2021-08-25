namespace WhackAStoodent.Client.Networking.Messages
{
    public class LoadedGameMessage : AMessage
    {
        public LoadedGameMessage() : base()
        {
        }

        public override EMessageType MessageType => EMessageType.LoadedGame;
    }
}
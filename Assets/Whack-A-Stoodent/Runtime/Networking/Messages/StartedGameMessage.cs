namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class StartedGameMessage : AMessage
    {
        
        public readonly EGameRole _playerGameRole;
        public readonly string _opponentName;
        
        public StartedGameMessage(EGameRole playerGameRole, string opponentName) : base()
        {
            _playerGameRole = playerGameRole;
            _opponentName = opponentName;
        }
        
        public override EMessageType MessageType => EMessageType.StartedGame;
    }
}
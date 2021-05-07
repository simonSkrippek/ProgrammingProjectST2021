namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public partial class PlayRequestMessage : AMessage
    {
        public readonly EGameRole _playerGameRole;
        public readonly string _opponentName;
        public readonly string _sessionCode;
        
        public PlayRequestMessage(EGameRole playerGameRole, string opponentName, string sessionCode) : base()
        {
            _playerGameRole = playerGameRole;
            _opponentName = opponentName;
            _sessionCode = sessionCode;
        }

        public override EMessageType MessageType => EMessageType.PlayRequest;
    }
}
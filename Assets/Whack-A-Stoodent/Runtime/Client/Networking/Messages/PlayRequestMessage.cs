namespace WhackAStoodent.Client.Networking.Messages
{
    public partial class PlayRequestMessage : AMessage
    {
        public readonly string _opponentName;
        public readonly string _sessionCode;
        
        public PlayRequestMessage(string opponentName, string sessionCode) : base()
        {
            _opponentName = opponentName;
            _sessionCode = sessionCode;
        }

        public override EMessageType MessageType => EMessageType.PlayRequest;
    }
}
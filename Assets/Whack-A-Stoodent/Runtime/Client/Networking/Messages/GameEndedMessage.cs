namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class GameEndedMessage : AMessage
    {
        public readonly MatchData _matchData;
        
        public GameEndedMessage(MatchData matchData) : base()
        {
            _matchData = matchData;
        }

        public override EMessageType MessageType => EMessageType.GameEnded;
    }
}
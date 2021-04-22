namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class GameEndedMessage : AMessage
    {
        public readonly MatchData _matchData;
        
        public GameEndedMessage(EMessagePurpose messagePurpose, MatchData matchData) : base(messagePurpose)
        {
            _matchData = matchData;
        }

        public override EMessageType MessageType => EMessageType.GameEnded;
    }
}
using System;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class MatchHistoryMessage : AMessage
    {
        public readonly MatchData[] _matchData;
        
        public MatchHistoryMessage(EMessagePurpose messagePurpose, MatchData[] matchData) : base(messagePurpose)
        {
            _matchData = matchData;
        }

        public override EMessageType MessageType => EMessageType.MatchHistory;
        
        
    }
}
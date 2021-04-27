﻿using System;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class MatchHistoryMessage : AMessage
    {
        public readonly MatchData[] _matchData;
        
        public MatchHistoryMessage(MatchData[] matchData) : base()
        {
            _matchData = matchData;
        }

        public override EMessageType MessageType => EMessageType.MatchHistory;
        
        
    }
}
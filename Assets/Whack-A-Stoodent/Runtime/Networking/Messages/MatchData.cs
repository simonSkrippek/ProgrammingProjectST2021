using System;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public readonly struct MatchData
    {
        public MatchData(Guid matchGuid, string playerName, long playerScore, string opponentName, long opponentScore)
        {
            _matchGuid = matchGuid;
            _playerName = playerName;
            _playerScore = playerScore;
            _opponentName = opponentName;
            _opponentScore = opponentScore;
        }

        public readonly Guid _matchGuid;
            
        public readonly string _playerName;
        public readonly long _playerScore;
            
        public readonly string _opponentName;
        public readonly long _opponentScore;
    }
}
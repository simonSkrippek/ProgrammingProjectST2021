using System;

namespace WhackAStoodent.Client.Networking.Messages
{
    public readonly struct MatchData
    {
        public MatchData(Guid matchGuid, string playerName, EGameRole playerRole, long playerScore, string opponentName, EGameRole opponentRole, long opponentScore)
        {
            _matchGuid = matchGuid;
            
            _playerName = playerName;
            _playerRole = playerRole;
            _playerScore = playerScore;
            
            _opponentName = opponentName;
            _opponentRole = opponentRole;
            _opponentScore = opponentScore;
        }

        public readonly Guid _matchGuid;
            
        public readonly string _playerName;
        public readonly EGameRole _playerRole;
        public readonly long _playerScore;
            
        public readonly string _opponentName;
        public readonly EGameRole _opponentRole;
        public readonly long _opponentScore;
    }
}
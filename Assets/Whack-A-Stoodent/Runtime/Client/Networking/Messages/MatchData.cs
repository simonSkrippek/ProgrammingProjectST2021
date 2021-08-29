using System;

namespace WhackAStoodent.Client.Networking.Messages
{
    public readonly struct MatchData
    {
        public MatchData(Guid matchGuid, string playerName, EGameRole playerGameRole, long playerScore, string opponentName, EGameRole opponentGameRole, long opponentScore)
        {
            _matchGuid = matchGuid;
            
            _playerName = playerName;
            _playerGameRole = playerGameRole;
            _playerScore = playerScore;
            
            _opponentName = opponentName;
            _opponentGameRole = opponentGameRole;
            _opponentScore = opponentScore;
        }

        public readonly Guid _matchGuid;
            
        public readonly string _playerName;
        public readonly EGameRole _playerGameRole;
        public readonly long _playerScore;
            
        public readonly string _opponentName;
        public readonly EGameRole _opponentGameRole;
        public readonly long _opponentScore;
    }
}
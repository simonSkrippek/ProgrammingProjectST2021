using System;
using Newtonsoft.Json;

namespace WhackAStoodent.Client.Networking.Messages
{
    public readonly struct MatchHistoryEntry
    {
        public MatchHistoryEntry(Guid matchGuid, DateTime playedDateTime, string playerName, EGameRole playerGameRole, long playerScore, string opponentName, EGameRole opponentGameRole, long opponentScore)
        {
            _matchGuid = matchGuid;

            _playedDateTime = playedDateTime;
            
            _playerName = playerName;
            _playerGameRole = playerGameRole;
            _playerScore = playerScore;
            
            _opponentName = opponentName;
            _opponentGameRole = opponentGameRole;
            _opponentScore = opponentScore;
        }

        [JsonProperty("sessionID")]
        public readonly Guid _matchGuid;
        
        [JsonProperty("playedDateTime")]
        public readonly DateTime _playedDateTime;
            
        [JsonProperty("yourName")]
        public readonly string _playerName;
        [JsonProperty("yourRole")]
        public readonly EGameRole _playerGameRole;
        [JsonProperty("yourScore")]
        public readonly long _playerScore;
            
        [JsonProperty("opponentName")]
        public readonly string _opponentName;
        [JsonProperty("opponentRole")]
        public readonly EGameRole _opponentGameRole;
        [JsonProperty("opponentScore")]
        public readonly long _opponentScore;
    }
}
using System;
using Newtonsoft.Json;

namespace WhackAStoodent.Client.Networking.Messages
{
    public readonly struct UserStats
    {
        public UserStats(Guid userGuid, ulong totalGamesPlayed, ulong gamesWon, ulong gamesLost, ulong gamesTied, DateTime lastOnline)
        {
            _userGuid = userGuid;
            
            _totalGamesPlayed = totalGamesPlayed;
            _gamesWon = gamesWon;
            _gamesLost = gamesLost;
            _gamesTied = gamesTied;
            
            _lastOnline = lastOnline;
        }

        [JsonProperty("userID")]
        public readonly Guid _userGuid;
        
        [JsonProperty("gamesPlayed")]
        public readonly ulong _totalGamesPlayed;
        [JsonProperty("gamesWon")]
        public readonly ulong _gamesWon;
        [JsonProperty("gamesLost")]
        public readonly ulong _gamesLost;
        [JsonProperty("gamesTied")]
        public readonly ulong _gamesTied;
        
        [JsonProperty("lastOnline")]
        public readonly DateTime _lastOnline;
    }
}
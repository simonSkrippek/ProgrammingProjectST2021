using System;
using Newtonsoft.Json;

namespace WhackAStoodent.Client.Networking.Messages
{
    public readonly struct UserStats
    {
        public UserStats(Guid userGuid, int totalGamesPlayed, int gamesWon, int gamesLost, DateTime lastOnline)
        {
            _userGuid = userGuid;
            
            _totalGamesPlayed = totalGamesPlayed;
            _gamesWon = gamesWon;
            _gamesLost = gamesLost;
            
            _lastOnline = lastOnline;
        }

        [JsonProperty("userID")]
        public readonly Guid _userGuid;
        
        [JsonProperty("gamesPlayed")]
        public readonly int _totalGamesPlayed;
        [JsonProperty("gamesWon")]
        public readonly int _gamesWon;
        [JsonProperty("gamesLost")]
        public readonly int _gamesLost;
        
        [JsonProperty("lastOnline")]
        public readonly DateTime _lastOnline;
    }
}
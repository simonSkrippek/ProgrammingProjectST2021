using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhackAStoodent.Client.Networking.Messages
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EGameRole
    {
        Hitter = 0,
        Mole = 1,
    }
}
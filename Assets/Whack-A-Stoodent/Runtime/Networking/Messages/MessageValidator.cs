namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class MessageValidator
    {
        public static readonly EMessageType[] ValidClientMessageTypes =
        {
            EMessageType.Authenticate, 
            EMessageType.Look,
            EMessageType.Ping,
            EMessageType.Pong,
            EMessageType.GetMatchHistory,
            EMessageType.PlayWithRandom,
            EMessageType.PlayWithSessionID,
            EMessageType.AcceptPlayRequest,
            EMessageType.DenyPlayRequest,
            EMessageType.LoadedGame,
            EMessageType.Hit,
            EMessageType.Look,
            EMessageType.Hide,
        };
        public static readonly EMessageType[] ValidServerMessageTypes =
        {
            EMessageType.AcknowledgeAuthentication,
            EMessageType.DenyAuthentication,
            EMessageType.Ping,
            EMessageType.Pong,
            EMessageType.MatchHistory,
            EMessageType.PlayRequest,
            EMessageType.LoadedGame,
            EMessageType.GameEnded,
            EMessageType.HitSuccess,
            EMessageType.HitFail,
            EMessageType.MoleScored
        };
        

    }
}
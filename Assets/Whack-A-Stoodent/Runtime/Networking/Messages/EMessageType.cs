namespace WhackAStoodent.Runtime.Networking.Messages
{
    public enum EMessageType
    {
        Authenticate,
        AcknowledgeAuthentication,
        DenyAuthentication,
        Ping,
        Pong,
        GetMatchHistory,
        MatchHistory,
        
        PlayWithRandom,
        PlayWithSessionID,
        PlayRequest,
        AcceptPlayRequest,
        DenyPlayRequest,
        LoadedGame,
        GameEnded,
        
        Hit,
        Look,
        Hide,
        HitSuccess,
        HitFail,
        MoleScored
    }
}
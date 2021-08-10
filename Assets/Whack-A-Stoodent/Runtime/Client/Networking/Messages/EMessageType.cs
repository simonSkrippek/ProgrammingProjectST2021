namespace WhackAStoodent.Client.Networking.Messages
{
    public enum EMessageType
    {
        Error,
        
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
        StartedGame,
        LoadedGame,
        GameEnded,
        
        Hit,
        Look,
        Hide,
        HitSuccess,
        HitFail,
        MoleScored,
        
        Debug = 255,
    }
}
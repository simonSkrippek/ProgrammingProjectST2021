namespace WhackAStoodent.GameState
{
    public enum EGameState
    {
        //not connected, game just started or was just reset. networking scene needs to be newly loaded
        PreConnect,
        //not connected, game just started or was just reset. networking scene needs to be newly loaded
        Connecting,
        //connected, but not authenticated -> needs to enter username to authenticate. back quits
        Unauthenticated,
            
        //connected, authenticated, doing menu stuff -> can request games or receive game requests. back bring up want to quit menu.
        Authenticated,
        //after sending a play request -> no action, can only wait for response. back does nothing?
        WaitingForAnswerToPlayRequest,
        //after receiving a play request -> action required, accept or deny. back denies
        AnsweringPlayRequest,
        //when loading, before playing -> may only send finished loading message, until receiving one. back does nothing
        LoadingPlaySession,
        //while playing -> may send gameplay messages and receive them, until game end messages
        InPlaySession,
        //same as authenticated -> may send gameplay messages and receive them, until game end messages
        InResultsScreen,
    }
}
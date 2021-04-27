namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PlayWithSessionIDMessage : AMessage
    {
        public readonly string _sessionID;
        
        public PlayWithSessionIDMessage(string sessionID) : base()
        {
            _sessionID = sessionID;
        }

        public override EMessageType MessageType => EMessageType.PlayWithSessionID;
    }
}
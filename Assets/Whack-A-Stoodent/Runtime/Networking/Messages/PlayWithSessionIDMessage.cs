namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PlayWithSessionIDMessage : AMessage
    {
        public readonly string _sessionID;
        
        public PlayWithSessionIDMessage(EMessagePurpose messagePurpose, string sessionID) : base(messagePurpose)
        {
            _sessionID = sessionID;
        }

        public override EMessageType MessageType => EMessageType.PlayWithSessionID;
    }
}
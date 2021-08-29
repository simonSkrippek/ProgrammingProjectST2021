using System;

namespace WhackAStoodent.Client.Networking.Messages
{
    public class AcknowledgeAuthenticationMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.AcknowledgeAuthentication;
        
        public readonly Guid _userID;
        public readonly string _userName;
        public readonly string _sessionCode;
        
        public AcknowledgeAuthenticationMessage(Guid userID, string userName, string sessionCode) : base()
        {
            _userID = userID;
            _userName = userName;
            _sessionCode = sessionCode;
        }
    }
}
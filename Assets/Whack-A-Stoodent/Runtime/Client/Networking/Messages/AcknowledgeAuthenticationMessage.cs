using System;

namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class AcknowledgeAuthenticationMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.AcknowledgeAuthentication;
        
        public readonly Guid _userID;
        public readonly string _userName;
        
        public AcknowledgeAuthenticationMessage(Guid userID, string userName) : base()
        {
            _userID = userID;
            _userName = userName;
        }
    }
}
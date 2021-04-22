using System;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class AcknowledgeAuthenticationMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.AcknowledgeAuthentication;
        
        public readonly Guid _userID;
        public readonly string _userName;
        
        public AcknowledgeAuthenticationMessage(EMessagePurpose messagePurpose, Guid userID, string userName) : base(messagePurpose)
        {
            _userID = userID;
            _userName = userName;
        }
    }
}
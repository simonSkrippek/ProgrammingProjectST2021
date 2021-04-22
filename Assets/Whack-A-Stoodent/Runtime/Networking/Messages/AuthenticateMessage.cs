using System;
using JetBrains.Annotations;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class AuthenticateMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.Authenticate;

        public readonly Guid _userID;
        public readonly string _userName;

        public AuthenticateMessage(EMessagePurpose messagePurpose, Guid? userID = null, [CanBeNull] string userName = null) : base(messagePurpose)
        {
            _userID = userID ?? new Guid(new byte[16]);
            _userName = userName;
        }
    }
}
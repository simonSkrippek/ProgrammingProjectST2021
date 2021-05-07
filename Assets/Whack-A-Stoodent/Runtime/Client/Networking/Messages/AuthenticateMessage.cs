using System;
using JetBrains.Annotations;

namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class AuthenticateMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.Authenticate;

        public readonly Guid _userID;
        public readonly string _userName;

        public AuthenticateMessage(Guid? userID = null, [CanBeNull] string userName = null) : base()
        {
            _userID = userID ?? new Guid(new byte[16]);
            _userName = userName;
        }
    }
}
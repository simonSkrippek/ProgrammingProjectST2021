using System;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PingMessage : AMessage
    {
        public readonly byte[] _pingData;
        
        public PingMessage(EMessagePurpose messagePurpose) : base(messagePurpose)
        {
            _pingData = new byte[4];
            var rnd = new Random();
            rnd.NextBytes(_pingData);
        }

        public override EMessageType MessageType => EMessageType.Ping;
    }
}
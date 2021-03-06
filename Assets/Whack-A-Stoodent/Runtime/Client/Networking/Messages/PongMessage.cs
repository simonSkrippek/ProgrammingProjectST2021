using System;

namespace WhackAStoodent.Client.Networking.Messages
{
    public class PongMessage : AMessage
    {
        public readonly byte[] _pingData;
        
        public PongMessage(byte[] pingData) : base()
        {
            if (pingData == null || pingData.Length != 4) throw new ArgumentException("pingData byte array passed in the creation of a pong message must have been extracted from a prior ping message");
            _pingData = pingData;
        }

        public override EMessageType MessageType => EMessageType.Pong;
    }
}
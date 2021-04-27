using System;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PingMessage : AMessage
    {
        public readonly byte[] _pingData;
        
        public PingMessage() : base()
        {
            _pingData = new byte[4];
            var rnd = new Random();
            rnd.NextBytes(_pingData);
        }
        public PingMessage(byte[] pingData) : base()
        {
            if (pingData == null || pingData.Length != 4) throw new ArgumentException("pingdata can be nothing but an array of 4 bytes with random values");
                _pingData = pingData;
        }

        public override EMessageType MessageType => EMessageType.Ping;
    }
}
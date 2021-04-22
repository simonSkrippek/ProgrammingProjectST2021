using ENet;

namespace WhackAStoodent.Runtime.Networking.Connectors
{
    internal enum EDisconnectionReason
    {
        GameFlow,
        InvalidPeer,
    }


    internal readonly struct ReceivedConnectionAttemptMessage
    {
        public Peer Sender { get; }

        public ReceivedConnectionAttemptMessage(Peer sender)
        {
            Sender = sender;
        }
    }

    internal readonly struct ReceivedDisconnectionMessage
    {
        public Peer Sender { get; }

        public ReceivedDisconnectionMessage(Peer sender)
        {
            Sender = sender;
        }
    }

    internal readonly struct ReceivedTimeOutMessage
    {
        public Peer Sender { get; }

        public ReceivedTimeOutMessage(Peer sender)
        {
            Sender = sender;
        }
    }

    internal readonly struct ReceivedCustomMessage
    {
        public Peer Sender { get; }
        public byte[] Message { get; }

        public ReceivedCustomMessage(Peer peer, byte[] message)
        {
            Sender = peer;
            Message = message;
        }
    }

    internal readonly struct CustomMessageToSend
    {
        public Peer Recipient { get; }
        public uint ChannelID { get; }
        public byte[] Message { get; }

        public CustomMessageToSend(Peer peer, uint channelID, byte[] message)
        {
            Recipient = peer;
            ChannelID = channelID;
            Message = message;
        }
    }
}
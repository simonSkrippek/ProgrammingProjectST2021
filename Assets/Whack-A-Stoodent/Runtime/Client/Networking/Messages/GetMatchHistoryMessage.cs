namespace WhackAStoodent.Runtime.Client.Networking.Messages
{
    public class GetMatchHistoryMessage : AMessage
    {
        public GetMatchHistoryMessage() : base()
        {
        }

        public override EMessageType MessageType => EMessageType.GetMatchHistory;
    }
}
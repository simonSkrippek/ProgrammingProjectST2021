namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class GetMatchHistoryMessage : AMessage
    {
        public GetMatchHistoryMessage(EMessagePurpose messagePurpose) : base(messagePurpose)
        {
        }

        public override EMessageType MessageType => EMessageType.GetMatchHistory;
    }
}
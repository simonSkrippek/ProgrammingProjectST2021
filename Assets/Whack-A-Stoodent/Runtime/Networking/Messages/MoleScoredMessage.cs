namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class MoleScoredMessage : AMessage
    {
        public readonly int _points;
        
        public MoleScoredMessage(EMessagePurpose messagePurpose, int points) : base(messagePurpose)
        {
            _points = points;
        }

        public override EMessageType MessageType => EMessageType.MoleScored;
    }
}
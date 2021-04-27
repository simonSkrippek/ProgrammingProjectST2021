namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class MoleScoredMessage : AMessage
    {
        public readonly long _pointsGained;
        
        public MoleScoredMessage(long pointsGained) : base()
        {
            _pointsGained = pointsGained;
        }

        public override EMessageType MessageType => EMessageType.MoleScored;
    }
}
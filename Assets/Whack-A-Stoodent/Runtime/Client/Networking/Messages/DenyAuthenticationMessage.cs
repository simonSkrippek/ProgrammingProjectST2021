namespace WhackAStoodent.Client.Networking.Messages
{
    public class DenyAuthenticationMessage : AMessage
    {
        public override EMessageType MessageType => EMessageType.DenyAuthentication;
        
        public DenyAuthenticationMessage() : base()
        {
            
        }
    }
}
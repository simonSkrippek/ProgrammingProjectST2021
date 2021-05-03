namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class DebugMessage : AMessage
    {
        public ELogLevel _logLevel;
        public string _message;

        public DebugMessage(ELogLevel logLevel, string message)
        {
            _logLevel = logLevel;
            _message = message;
        }

        public override EMessageType MessageType => EMessageType.Debug;
    }
}
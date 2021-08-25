namespace WhackAStoodent.Client.Networking.Messages
{
    public class ErrorMessage : AMessage
    {
        public EErrorType _type;
        public string _message;

        public ErrorMessage(EErrorType type, string message)
        {
            _type = type;
            _message = message;
        }

        public override EMessageType MessageType => EMessageType.Error;
    }
}
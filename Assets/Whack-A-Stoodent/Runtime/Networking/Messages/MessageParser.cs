namespace WhackAStoodent.Runtime.Networking.Messages
{
    public static class MessageParser
    {
        public static bool ParseMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            messageBytes = null;
            return false;
        }
        public static bool ParseMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            parsedMessage = null;
            return false;
        }
    }
}
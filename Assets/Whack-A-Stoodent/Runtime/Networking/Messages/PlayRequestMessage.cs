﻿namespace WhackAStoodent.Runtime.Networking.Messages
{
    public class PlayRequestMessage : AMessage
    {
        public readonly GameRole _playerGameRole;
        public readonly string _opponentName;
        
        public PlayRequestMessage(EMessagePurpose messagePurpose, GameRole playerGameRole, string opponentName) : base(messagePurpose)
        {
            _playerGameRole = playerGameRole;
            _opponentName = opponentName;
        }

        public override EMessageType MessageType => EMessageType.PlayRequest;

        public enum GameRole
        {
            Hitter = 0,
            Mole = 1,
        }
    }
}
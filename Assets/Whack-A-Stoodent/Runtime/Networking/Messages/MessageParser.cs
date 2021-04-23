using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public static class MessageParser
    {
        private static UnicodeEncoding _unicodeEncoder = new UnicodeEncoding(false, true);
        
        public static bool ParseMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            switch (messageToParse.MessageType)
            {
                case EMessageType.Authenticate:
                    if (TryParseAuthenticateMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.AcknowledgeAuthentication:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.DenyAuthentication:
                    throw new ArgumentException("should not need to parse a message with messageType 'DenyAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.Ping:
                    if (TryParsePingMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Pong:
                    if (TryParsePongMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.GetMatchHistory:
                    if (TryParseGetMatchHistoryMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.MatchHistory:
                    throw new ArgumentException("should not need to parse a message with messageType 'MatchHistory' in this direction (message to byte)");
                    break;
                case EMessageType.PlayWithRandom:
                    if (TryParsePlayWithRandomMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.PlayWithSessionID:
                    if (TryParsePlayWithSessionIDMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.PlayRequest:
                    throw new ArgumentException("should not need to parse a message with messageType 'PlayRequest' in this direction (message to byte)");
                    break;
                case EMessageType.AcceptPlayRequest:
                    if (TryParseAcceptPlayRequestMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.DenyPlayRequest:
                    if (TryParseDenyPlayRequestMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.LoadedGame:
                    if (TryParseLoadedGameMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.GameEnded:
                    throw new ArgumentException("should not need to parse a message with messageType 'GameEnded' in this direction (message to byte)");
                    break;
                case EMessageType.Hit:
                    if (TryParseHitMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Look:
                    if (TryParseLookMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Hide:
                    if (TryParseHideMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.HitSuccess:
                    throw new ArgumentException("should not need to parse a message with messageType 'HitSuccess' in this direction (message to byte)");
                    break;
                case EMessageType.HitFail:
                    throw new ArgumentException("should not need to parse a message with messageType 'HitFail' in this direction (message to byte)");
                    break;
                case EMessageType.MoleScored:
                    throw new ArgumentException("should not need to parse a message with messageType 'MoleScored' in this direction (message to byte)");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            messageBytes = null;
            return false;
        }
        private static bool TryParseAuthenticateMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is AuthenticateMessage authenticate_message_to_parse)
            {
                int required_number_of_bytes = 18 + authenticate_message_to_parse._userName.Length * 2;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Authenticate;

                authenticate_message_to_parse._userID.ToByteArray().CopyTo(messageBytes, 1);
                messageBytes[17] = (byte) authenticate_message_to_parse._userName.Length;
                _unicodeEncoder.GetBytes(authenticate_message_to_parse._userName).CopyTo(messageBytes, 18);

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParsePingMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is PingMessage ping_message_to_parse)
            {
                int required_number_of_bytes = 5;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Ping;

                ping_message_to_parse._pingData.CopyTo(messageBytes, 1);

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParsePongMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is PongMessage pong_message_to_parse)
            {
                int required_number_of_bytes = 5;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Pong;

                pong_message_to_parse._pingData.CopyTo(messageBytes, 1);

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseGetMatchHistoryMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is GetMatchHistoryMessage)
            {
                int required_number_of_bytes = 1;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.GetMatchHistory;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParsePlayWithRandomMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is PlayWithRandomMessage)
            {
                int required_number_of_bytes = 1;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.PlayWithRandom;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParsePlayWithSessionIDMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is PlayWithSessionIDMessage play_with_session_id_message_to_parse)
            {
                int required_number_of_bytes = 5;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.PlayWithSessionID;

                Encoding.ASCII.GetBytes(play_with_session_id_message_to_parse._sessionID).CopyTo(messageBytes, 1);

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseAcceptPlayRequestMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is AcceptPlayRequestMessage)
            {
                int required_number_of_bytes = 1;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.AcceptPlayRequest;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseDenyPlayRequestMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is DenyPlayRequestMessage deny_play_request_message)
            {
                int required_number_of_bytes = 2;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.DenyPlayRequest;

                messageBytes[1] = (byte) deny_play_request_message._denialReason;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseLoadedGameMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is LoadedGameMessage loaded_game_message_to_parse)
            {
                int required_number_of_bytes = 1;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.LoadedGame;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseHitMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is HitMessage hit_message_to_parse)
            {
                int required_number_of_bytes = 9;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Hit;

                BitConverter.GetBytes(hit_message_to_parse._position.x).CopyTo(messageBytes, 1);
                BitConverter.GetBytes(hit_message_to_parse._position.y).CopyTo(messageBytes, 5);

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseLookMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is LookMessage look_message_to_parse)
            {
                int required_number_of_bytes = 2;
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Look;

                messageBytes[1] = (byte) look_message_to_parse._holeIndex;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseHideMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is HideMessage)
            {
                int requiredNumberOfBytes = 1;
                messageBytes = new byte[requiredNumberOfBytes];
                messageBytes[0] = (byte) EMessageType.Hide;

                return true;
            }

            messageBytes = null;
            return false;
        }

        public static bool ParseMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes == null || messageBytes.Length == 0)
            {
                parsedMessage = null;
                return false;
            }

            EMessageType message_type = (EMessageType) messageBytes[0];
            switch (message_type)
            {
                case EMessageType.Authenticate:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.AcknowledgeAuthentication:
                    if (TryParseAcknowledgeAuthenticationMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.DenyAuthentication:
                    if (TryParseDenyAuthenticationMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Ping:
                    break;
                case EMessageType.Pong:
                    break;
                case EMessageType.GetMatchHistory:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.MatchHistory:
                    break;
                case EMessageType.PlayWithRandom:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.PlayWithSessionID:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.PlayRequest:
                    break;
                case EMessageType.AcceptPlayRequest:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.DenyPlayRequest:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.LoadedGame:
                    break;
                case EMessageType.GameEnded:
                    break;
                case EMessageType.Hit:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.Look:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.Hide:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.HitSuccess:
                    break;
                case EMessageType.HitFail:
                    break;
                case EMessageType.MoleScored:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            parsedMessage = null;
            return false;
        }

        

        private static bool TryParseAcknowledgeAuthenticationMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int minimum_required_message_bytes = 18;
            if (messageBytes.Length < minimum_required_message_bytes)
            {
                parsedMessage = null;
                return false;
            }

            var guid = new Guid(GetSubArray(messageBytes, 1, 16));
            var name_length = messageBytes[17];
            if (messageBytes.Length < minimum_required_message_bytes + name_length * 2)
            {
                parsedMessage = null;
                return false;
            }
            var name = new string(_unicodeEncoder.GetChars(GetSubArray(messageBytes, 18, name_length * 2)));
            
            parsedMessage = new AcknowledgeAuthenticationMessage(EMessagePurpose.Received, guid, name);
            return true;
        }
        private static bool TryParseDenyAuthenticationMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            parsedMessage = new AuthenticateMessage(EMessagePurpose.Received);
            return true;
        }
        private static bool TryParsePingMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int required_message_bytes = 5;
            if (messageBytes.Length != required_message_bytes)
            {
                parsedMessage = null;
                return false;
            }
            
            var ping_payload = GetSubArray(messageBytes, 1, 4);
            
            parsedMessage = new PingMessage(EMessagePurpose.Received, ping_payload);
            return true;
        }
        private static bool TryParsePongMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int required_message_bytes = 5;
            if (messageBytes.Length != required_message_bytes)
            {
                parsedMessage = null;
                return false;
            }
            
            var ping_payload = GetSubArray(messageBytes, 1, 4);
            
            parsedMessage = new PongMessage(EMessagePurpose.Received, ping_payload);
            return true;
        }
        private static bool TryParseMatchHistoryMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int minimum_required_message_bytes = 5;
            if (messageBytes.Length < minimum_required_message_bytes)
            {
                parsedMessage = null;
                return false;
            }

            uint entry_count = BitConverter.ToUInt32(GetSubArray(messageBytes, 1, 4), 0);
            if(TryParseMatchData(GetSubArray(messageBytes, 5, messageBytes.Length - 5), out MatchData[] match_data))
            {
                parsedMessage = new MatchHistoryMessage(EMessagePurpose.Received, new MatchData[0]);
                return true;
            }
            else
            {
                parsedMessage = default;
                return false;
            }
        }
        private static bool TryParseMatchData(byte[] messageBytes, out MatchData[] matchData)
        {
            int current_byte_index = 0;
            while (expression)
            {
                
            }
            int minimum_required_message_bytes = 34;
            if (messageBytes.Length < minimum_required_message_bytes)
            {
                matchData = default;
                return false;
            }
            

            var session_guid = new Guid(GetSubArray(messageBytes, 0, 16));
            long player_score = BitConverter.ToInt64(messageBytes, 16);
            byte player_name_length = messageBytes[24];
            
            if (messageBytes.Length < minimum_required_message_bytes + player_name_length * 2)
            {
                matchData = default;
                return false;
            }
            var player_name = new string(_unicodeEncoder.GetChars(GetSubArray(messageBytes, 25, player_name_length * 2)));

            int opponent_data_start_index = minimum_required_message_bytes + player_name_length * 2;
            
            long opponent_score = BitConverter.ToInt64(messageBytes, opponent_data_start_index);
            byte opponent_name_length = messageBytes[opponent_data_start_index + 8];
            
            if (messageBytes.Length < opponent_data_start_index + opponent_name_length * 2)
            {
                matchData = default;
                return false;
            }
            var opponent_name = new string(_unicodeEncoder.GetChars(GetSubArray(messageBytes, opponent_data_start_index + 9, opponent_name_length * 2)));
            
            matchData = new MatchData(session_guid, player_name, player_score, opponent_name, opponent_score);
            return true;
        }
        

        private static byte[] GetSubArray(byte[] originalArray, int startIndex, int length)
        {
            var ret = new byte[length];
            Array.Copy(originalArray, startIndex, ret, 0, length);
            return ret;
        }
        private static byte[] GetSubArray(byte[] originalArray, uint startIndex, uint length)
        {
            var ret = new byte[length];
            Array.Copy(originalArray, startIndex, ret, 0, length);
            return ret;
        }
    }
}
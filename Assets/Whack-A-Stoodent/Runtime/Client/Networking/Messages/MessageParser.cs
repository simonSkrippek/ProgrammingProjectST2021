using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Client.Networking.Messages
{
    public static class MessageParser
    {
        // PARSING FROM MESSAGE OBJECTS TO BYTE ARRAY; CLIENT TO SERVER MESSAGES
        public static bool ParseMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            switch (messageToParse.MessageType)
            {
                case EMessageType.Error:
                    if (TryParseErrorMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Authenticate:
                    if (TryParseAuthenticateMessage(messageToParse, out messageBytes)) return true;
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
                case EMessageType.PlayWithRandom:
                    if (TryParsePlayWithRandomMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.PlayWithSessionID:
                    if (TryParsePlayWithSessionIDMessage(messageToParse, out messageBytes)) return true;
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
                case EMessageType.Hit:
                    if (TryParseHitMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Look:
                    if (TryParseLookMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Hide:
                    if (TryParseHideMessage(messageToParse, out messageBytes)) return true;
                    break;
                case EMessageType.Debug:
                    if (TryParseDebugMessage(messageToParse, out messageBytes)) return true;
                    break;
                default:
                    throw new ArgumentException($"should not need to parse a message with messageType {messageToParse.MessageType} in this direction (message to byte)");
            }
            
            messageBytes = null;
            return false;
        }
        private static bool TryParseErrorMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is ErrorMessage error_message_to_parse)
            {
                int required_number_of_bytes = 2 + GetDynamicallySizedStringEncodingLength(EDynamicStringSize.Int, error_message_to_parse._message);
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Error;
                messageBytes[1] = (byte) error_message_to_parse._type;

                return TryParseDynamicallySizedString(messageBytes, error_message_to_parse._message, 2, out _, EDynamicStringSize.Int);
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseAuthenticateMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is AuthenticateMessage authenticate_message_to_parse)
            {
                int required_number_of_bytes = 17 + GetDynamicallySizedStringEncodingLength(EDynamicStringSize.Bit, authenticate_message_to_parse._userName);
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Authenticate;

                authenticate_message_to_parse._userID.ToByteArray().CopyTo(messageBytes, 1);
                messageBytes[17] = (byte) authenticate_message_to_parse._userName.Length;
                return TryParseDynamicallySizedString(messageBytes, authenticate_message_to_parse._userName, 2, out _);
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
            if (messageToParse is AcceptPlayRequestMessage accept_play_request_message)
            {
                messageBytes = new byte[1];
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
                messageBytes = new byte[2];
                messageBytes[0] = (byte) EMessageType.DenyPlayRequest;
                messageBytes[1] = (byte) deny_play_request_message._denialReason;

                return true;
            }

            messageBytes = null;
            return false;
        }
        private static bool TryParseLoadedGameMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is LoadedGameMessage)
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
        private static bool TryParseDebugMessage(AMessage messageToParse, out byte[] messageBytes)
        {
            if (messageToParse is DebugMessage debug_message_to_parse)
            {
                int required_number_of_bytes = 2 + GetDynamicallySizedStringEncodingLength(EDynamicStringSize.Int, debug_message_to_parse._message);
                messageBytes = new byte[required_number_of_bytes];
                messageBytes[0] = (byte) EMessageType.Debug;
                messageBytes[1] = (byte) debug_message_to_parse._logLevel;

                return TryParseDynamicallySizedString(messageBytes, debug_message_to_parse._message, 2, out _, EDynamicStringSize.Int);
            }

            messageBytes = null;
            return false;
        }

        // PARSING FROM BYTE ARRAY TO MESSAGE OBJECT; SERVER TO CLIENT MESSAGES
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
                case EMessageType.Error:
                    if (TryParseErrorMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.AcknowledgeAuthentication:
                    if (TryParseAcknowledgeAuthenticationMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.DenyAuthentication:
                    if (TryParseDenyAuthenticationMessage(out parsedMessage)) return true;
                    break;
                case EMessageType.Ping:
                    if (TryParsePingMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Pong:
                    if (TryParsePongMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.MatchHistory:
                    if (TryParseMatchHistoryMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.PlayRequest:
                    if (TryParsePlayRequestMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.DenyPlayRequest:
                    if (TryParseDenyPlayRequestMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.StartedGame:
                    if (TryParseStartedGameMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.LoadedGame:
                    if (TryParseLoadedGameMessage(out parsedMessage)) return true;
                    break;
                case EMessageType.GameEnded:
                    if (TryParseGameEndedMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Look:
                    if (TryParseLookMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Hide:
                    if (TryParseHideMessage(out parsedMessage)) return true;
                    break;
                case EMessageType.HitSuccess:
                    if (TryParseHitSuccessMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.HitFail:
                    if (TryParseHitFailMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.MoleScored:
                    if (TryParseMoleScoredMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Debug:
                    if (TryParseDebugMessage(messageBytes, out parsedMessage)) return true;
                    break;
                default:
                    throw new ArgumentException($"should not need to parse a message with messageType {message_type} in this direction (byte to message)");
            }

            parsedMessage = null;
            return false;
        }
        private static bool TryParseErrorMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 2)
            {
                parsedMessage = null;
                return false;
            }
            int current_byte_index = 1;
            var error_type = (EErrorType) messageBytes[current_byte_index];
            current_byte_index++;
            if (!TryParseDynamicallySizedString(messageBytes, out string error_message, current_byte_index, out _))
            {
                parsedMessage = null;
                return false;
            }
            
            parsedMessage = new ErrorMessage(error_type, error_message);
            return true;
        }
        private static bool TryParseAcknowledgeAuthenticationMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int minimum_required_message_bytes = 18;
            if (messageBytes.Length < minimum_required_message_bytes)
            {
                parsedMessage = null;
                return false;
            }

            var guid = new Guid(messageBytes.GetSubArray(1, 16));
            //parse 6 byte long session code to string
            if (!TryParseSessionCode(messageBytes, out string session_code, 17, out int name_start_index) || 
                !TryParseDynamicallySizedString(messageBytes, out string name, name_start_index, out _))
            {
                parsedMessage = null;
                return false;
            }
            
            parsedMessage = new AcknowledgeAuthenticationMessage(guid, name, session_code);
            return true;
        }
        private static bool TryParseDenyAuthenticationMessage(out AMessage parsedMessage)
        {
            parsedMessage = new AuthenticateMessage();
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
            
            var ping_payload = messageBytes.GetSubArray(1, 4);
            
            parsedMessage = new PingMessage(ping_payload);
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
            
            var ping_payload = messageBytes.GetSubArray(1, 4);
            
            parsedMessage = new PongMessage(ping_payload);
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

            uint entry_count = BitConverter.ToUInt32(messageBytes, 1);
            if(TryParseMatchData(messageBytes.GetSubArray(5, messageBytes.Length - 5), entry_count, out MatchData[] match_data))
            {
                parsedMessage = new MatchHistoryMessage(match_data);
                return true;
            }
            else
            {
                parsedMessage = default;
                return false;
            }
        }
        private static bool TryParseMatchData(byte[] messageBytes, uint expectedNumberOfDataPoints, out MatchData[] matchData)
        {
            int current_byte_index = 0;
            List<MatchData> match_data = new List<MatchData>();
            for(uint i = 0; i < expectedNumberOfDataPoints; i++)
            {
                //session id parsing
                if (current_byte_index + 16 > messageBytes.Length)
                {
                    break;
                }
                var session_guid = new Guid(messageBytes.GetSubArray(current_byte_index, 16));
                current_byte_index += 16;

                //player score parsing
                if (current_byte_index + 8 > messageBytes.Length)
                {
                    break;
                }
                long player_score = BitConverter.ToInt64(messageBytes, current_byte_index);
                current_byte_index += 8;
                
                //player role parsing
                if (current_byte_index + 1 > messageBytes.Length)
                {
                    break;
                }
                EGameRole player_game_role = (EGameRole) messageBytes[current_byte_index];
                current_byte_index += 1;
                
                //player name parsing
                
                if (!TryParseDynamicallySizedString(messageBytes, out string player_name, current_byte_index, out int new_byte_index))
                {
                    break;
                }
                
                current_byte_index = new_byte_index;

                //opponent score parsing
                if (current_byte_index + 8 > messageBytes.Length)
                {
                    break;
                }
                long opponent_score = BitConverter.ToInt64(messageBytes, current_byte_index);
                current_byte_index += 8;
                
                //opponent role parsing
                if (current_byte_index + 1 > messageBytes.Length)
                {
                    break;
                }
                EGameRole opponent_game_role = (EGameRole) messageBytes[current_byte_index];
                current_byte_index += 1;
                
                //opponent name parsing
                if (!TryParseDynamicallySizedString(messageBytes, out string opponent_name, current_byte_index, out new_byte_index))
                {
                    break;
                }
                
                current_byte_index = new_byte_index;

                match_data.Add(new MatchData(session_guid, player_name, player_game_role, player_score, opponent_name, opponent_game_role, opponent_score));
            }

            if (current_byte_index == messageBytes.Length)
            {
                matchData = match_data.ToArray();
                return true;
            }
            else
            {
                matchData = null;
                return false;
            }
        }
        private static bool TryParsePlayRequestMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int current_byte_index = 1; 
            
            //session code parsing
            if (!TryParseSessionCode(messageBytes, out string session_code, current_byte_index, out int new_byte_index))
            {
                parsedMessage = null;
                return false;
            }
            current_byte_index = new_byte_index;
                
            //player name parsing
            if (!TryParseDynamicallySizedString(messageBytes, out string opponent_name, current_byte_index, out _))
            {
                parsedMessage = null;
                return false;
            }

            parsedMessage = new PlayRequestMessage(opponent_name, session_code);
            return true;
        }
        private static bool TryParseDenyPlayRequestMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 2)
            {
                parsedMessage = null;
                return false;
            }

            parsedMessage = new DenyPlayRequestMessage((DenyPlayRequestMessage.EDenialReason) messageBytes[1]);
            return true;
        }
        private static bool TryParseStartedGameMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            int current_byte_index = 1; 
            
            //player role index parsing
            if (current_byte_index + 1 > messageBytes.Length)
            {
                parsedMessage = null;
                return false;
            }
            
            EGameRole player_game_role = (EGameRole)messageBytes[1];
            current_byte_index += 1;
                
            //player name parsing
            if (!TryParseDynamicallySizedString(messageBytes, out string opponent_name, current_byte_index, out _))
            {
                parsedMessage = null;
                return false;
            }
            
            parsedMessage = new StartedGameMessage(player_game_role, opponent_name);
            return true;
        }
        private static bool TryParseLoadedGameMessage(out AMessage parsedMessage)
        {
            parsedMessage = new LoadedGameMessage();
            return true;
        }
        private static bool TryParseGameEndedMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if(TryParseMatchData(messageBytes.GetSubArray(1, messageBytes.Length - 1), 1, out MatchData[] match_data))
            {
                parsedMessage = new GameEndedMessage(match_data[0]);
                return true;
            }
            else
            {
                parsedMessage = null;
                return false;
            }
        }
        private static bool TryParseLookMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 2)
            {
                parsedMessage = null;
                return false;
            }

            EHoleIndex hole_index = (EHoleIndex) messageBytes[1];
            parsedMessage = new LookMessage(hole_index);
            return true;
        }
        private static bool TryParseHideMessage(out AMessage parsedMessage)
        {
            parsedMessage = new HideMessage();
            return true;
        }
        private static bool TryParseHitSuccessMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 18)
            {
                parsedMessage = null;
                return false;
            }
            
            EHoleIndex hole_index = (EHoleIndex) messageBytes[1];
            long points_gained =  BitConverter.ToInt64(messageBytes, 2);
            Vector2 position =  new Vector2(BitConverter.ToSingle(messageBytes, 10), BitConverter.ToInt64(messageBytes, 14));
            
            parsedMessage = new HitSuccessMessage(hole_index, points_gained, position);
            return true;
        }
        private static bool TryParseHitFailMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 18)
            {
                parsedMessage = null;
                return false;
            }
            
            Vector2 position =  new Vector2(BitConverter.ToSingle(messageBytes, 1), BitConverter.ToInt64(messageBytes, 5));
            
            parsedMessage = new HitFailMessage(position);
            return true;
        }
        private static bool TryParseMoleScoredMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 9)
            {
                parsedMessage = null;
                return false;
            }

            long points_gained = BitConverter.ToInt64(messageBytes, 1);
            
            parsedMessage = new MoleScoredMessage(points_gained);
            return true;
        }
        private static bool TryParseDebugMessage(byte[] messageBytes, out AMessage parsedMessage)
        {
            if (messageBytes.Length < 2)
            {
                parsedMessage = null;
                return false;
            }
            int current_byte_index = 1;
            var log_level = (ELogLevel) messageBytes[current_byte_index];
            current_byte_index++;
            if (!TryParseDynamicallySizedString(messageBytes, out string debug_message, current_byte_index, out _))
            {
                parsedMessage = null;
                return false;
            }
            
            parsedMessage = new DebugMessage(log_level, debug_message);
            return true;
        }

        private static bool TryParseDynamicallySizedString(byte[] buffer, string dynamicString, int startIndex, out int newIndex, EDynamicStringSize sizeType = EDynamicStringSize.Bit)
        {
            if (startIndex + GetDynamicallySizedStringEncodingLength(sizeType, dynamicString) > buffer.Length)
            {
                newIndex = 0;
                return false;
            }

            switch (sizeType)
            {
                case EDynamicStringSize.Bit:
                    buffer[startIndex] = (byte) dynamicString.Length;
                    startIndex += 1;
                    break;
                case EDynamicStringSize.Int:
                    BitConverter.GetBytes(dynamicString.Length).CopyTo(buffer, startIndex);
                    startIndex += 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeType), sizeType, null);
            }
            
            newIndex = startIndex + GetDynamicallySizedStringEncodingLength(sizeType, dynamicString);
            Encoding.Unicode.GetBytes(dynamicString).CopyTo(buffer, startIndex);

            return true;
        }
        private static bool TryParseDynamicallySizedString(byte[] buffer, out string dynamicString, int startIndex, out int newIndex, EDynamicStringSize sizeType = EDynamicStringSize.Bit)
        {
            if (startIndex + GetSizeEncodingLength(sizeType) > buffer.Length)
            {
                newIndex = 0;
                dynamicString = null;
                return false;
            }
            
            int dynamic_string_length;
            switch (sizeType)
            {
                case EDynamicStringSize.Bit:
                    dynamic_string_length = buffer[startIndex] * 2;
                    startIndex += 1;
                    break;
                case EDynamicStringSize.Int:
                    dynamic_string_length = BitConverter.ToInt32(buffer, startIndex) * 2;
                    startIndex += 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeType), sizeType, null);
            }
            
            if (startIndex + dynamic_string_length > buffer.Length)
            {
                newIndex = 0;
                dynamicString = null;
                return false;
            }
            
            dynamicString = Encoding.Unicode.GetString(buffer, startIndex, dynamic_string_length);
            newIndex = startIndex + dynamic_string_length;
            return true;
        }

        private static bool TryParseSessionCode(byte[] buffer, string sessionCode, int startIndex, out int newIndex)
        {
            newIndex = startIndex + GetSessionCodeEncodingLength();
            if (sessionCode == null || sessionCode.Length != GetSessionCodeStringLength() || buffer == null || buffer.Length < newIndex)
            {
                return false;
            }
            Encoding.UTF8.GetBytes(sessionCode).CopyTo(buffer, startIndex);
            return true;
        }
        private static bool TryParseSessionCode(byte[] buffer, out string sessionCode, int startIndex, out int newIndex)
        {
            newIndex = startIndex + GetSessionCodeEncodingLength();
            if (buffer == null || buffer.Length < newIndex)
            {
                sessionCode = null;
                return false;
            }

            sessionCode = Encoding.UTF8.GetString(buffer, startIndex, GetSessionCodeEncodingLength());
            return true;
        }

        private static int GetSessionCodeEncodingLength() => 6;
        private static int GetSessionCodeStringLength() => 6;

        private static int GetDynamicallySizedStringEncodingLength(EDynamicStringSize size, string dynamicString)
        {
            return GetSizeEncodingLength(size) + dynamicString.Length * 2;
        }
        private static int GetSizeEncodingLength(EDynamicStringSize size)
        {
            return
                size switch
                {
                    EDynamicStringSize.Bit => 1,
                    EDynamicStringSize.Int => 4,
                    _ => throw new ArgumentOutOfRangeException(nameof(size), size, "unrecognized string size")
                };
        }

        private enum EDynamicStringSize
        {
            Bit,
            Int,
        }
    }
}
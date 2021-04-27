using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WhackAStoodent.Runtime.Helper;

namespace WhackAStoodent.Runtime.Networking.Messages
{
    public static class MessageParser
    {
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
                Encoding.Unicode.GetBytes(authenticate_message_to_parse._userName).CopyTo(messageBytes, 18);

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
                    if (TryParsePingMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Pong:
                    if (TryParsePongMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.GetMatchHistory:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.MatchHistory:
                    if (TryParseMatchHistoryMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.PlayWithRandom:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.PlayWithSessionID:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.PlayRequest:
                    if (TryParsePlayRequestMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.AcceptPlayRequest:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.DenyPlayRequest:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.LoadedGame:
                    if (TryParseLoadedGameMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.GameEnded:
                    if (TryParseGameEndedMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Hit:
                    throw new ArgumentException("should not need to parse a message with messageType 'AcknowledgeAuthentication' in this direction (message to byte)");
                    break;
                case EMessageType.Look:
                    if (TryParseLookMessage(messageBytes, out parsedMessage)) return true;
                    break;
                case EMessageType.Hide:
                    if (TryParseHideMessage(messageBytes, out parsedMessage)) return true;
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

            var guid = new Guid(messageBytes.GetSubArray(1, 16));
            var name_length = messageBytes[17];
            if (messageBytes.Length < minimum_required_message_bytes + name_length * 2)
            {
                parsedMessage = null;
                return false;
            }
            var name = Encoding.Unicode.GetString(messageBytes.GetSubArray(18, name_length * 2));
            
            parsedMessage = new AcknowledgeAuthenticationMessage(guid, name);
            return true;
        }
        private static bool TryParseDenyAuthenticationMessage(byte[] messageBytes, out AMessage parsedMessage)
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
                parsedMessage = new MatchHistoryMessage(new MatchData[0]);
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
                
                //player name parsing
                if (current_byte_index + 1 > messageBytes.Length)
                {
                    break;
                }
                byte player_name_length = messageBytes[current_byte_index];
                current_byte_index += 1;

                int player_name_bytes = player_name_length * 2; 
                
                if (current_byte_index + player_name_bytes > messageBytes.Length)
                {
                    break;
                }
                var player_name = Encoding.Unicode.GetString(messageBytes.GetSubArray(current_byte_index, player_name_bytes));
                current_byte_index += player_name_bytes;

                //opponent score parsing
                if (current_byte_index + 8 > messageBytes.Length)
                {
                    break;
                }
                long opponent_score = BitConverter.ToInt64(messageBytes, current_byte_index);
                current_byte_index += 8;
                
                //opponent name parsing
                if (current_byte_index + 1 > messageBytes.Length)
                {
                    break;
                }
                byte opponent_name_length = messageBytes[current_byte_index];
                current_byte_index += 1;

                int opponent_name_bytes = opponent_name_length * 2; 
                
                if (current_byte_index + opponent_name_bytes > messageBytes.Length)
                {
                    break;
                }
                var opponent_name = Encoding.Unicode.GetString(messageBytes.GetSubArray(current_byte_index, opponent_name_bytes));
                current_byte_index += opponent_name_bytes;

                match_data.Add(new MatchData(session_guid, player_name, player_score, opponent_name, opponent_score));
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
            
            //player role index parsing
            if (current_byte_index + 1 > messageBytes.Length)
            {
                parsedMessage = null;
                return false;
            }
            
            PlayRequestMessage.GameRole player_game_role = (PlayRequestMessage.GameRole)messageBytes[1];
            current_byte_index += 1;
                
            //player name parsing
            if (current_byte_index + 1 > messageBytes.Length)
            {
                parsedMessage = null;
                return false;
            }
            byte opponent_name_length = messageBytes[current_byte_index];
            current_byte_index += 1;

            int opponent_name_bytes = opponent_name_length * 2; 
                
            if (current_byte_index + opponent_name_bytes > messageBytes.Length)
            {
                parsedMessage = null;
                return false;
            }
            var opponent_name = Encoding.Unicode.GetString(messageBytes.GetSubArray(current_byte_index, opponent_name_bytes));
            current_byte_index += opponent_name_bytes;
            
            parsedMessage = new PlayRequestMessage(player_game_role, opponent_name);
            return true;
        }
        private static bool TryParseLoadedGameMessage(byte[] messageBytes, out AMessage parsedMessage)
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
        private static bool TryParseHideMessage(byte[] messageBytes, out AMessage parsedMessage)
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
    }
}
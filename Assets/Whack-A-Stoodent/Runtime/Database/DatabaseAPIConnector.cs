using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Database
{
    public class DatabaseAPIConnector : MonoBehaviour
    {
        private const string API_VERSION = "v1";
        [SerializeField] private string serverAddress = "https://api.bigeti.de/whackastoodentstats";
        [SerializeField] private MatchDataEvent gameEndedEvent;
        [SerializeField] private MatchHistoryEntryArrayEvent matchHistoryReceivedEvent;
        [SerializeField] private UserStatsEvent userStatsReceivedEvent;
        private string APIAddress => serverAddress + "/" + API_VERSION;

        private void OnEnable()
        {
            gameEndedEvent.Subscribe(HandleGameEnded);
        }
        private void OnDisable()
        {
            gameEndedEvent.Unsubscribe(HandleGameEnded);
        }
        
        [ContextMenu("Test PostGameResult")]
        private void PostGameResultTest()
        {
            HandleGameEnded(new MatchData(new Guid(), "playerName", EGameRole.Mole, 10, "opponent", EGameRole.Hitter, 12));
        }
        private void HandleGameEnded(MatchData matchData)
        {
            var coroutine = PostGameResult(matchData);
            StartCoroutine(coroutine);
        }
        private IEnumerator PostGameResult(MatchData matchData)
        {
            UnityWebRequest request = UnityWebRequest.Post(APIAddress + $"/history?userid={StorageUtility.LoadClientGuid()}", JsonConvert.SerializeObject(matchData));
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
        }

        [ContextMenu("Test RequestMatchHistory")]
        private void RequestMatchHistoryTest() => RequestMatchHistory();
        public void RequestMatchHistory(ulong maxCount = 32)
        {
            var coroutine = GetMatchHistory(maxCount);
            StartCoroutine(coroutine);
        }
        private IEnumerator GetMatchHistory(ulong count)
        {
            UnityWebRequest request = UnityWebRequest.Get(APIAddress + $"/history?userid={StorageUtility.LoadClientGuid()}&count={count}");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                MatchHistoryEntry[] match_data = JsonConvert.DeserializeObject<MatchHistoryEntry[]>(request.downloadHandler.text);
                matchHistoryReceivedEvent.Invoke(match_data);
            }
        }
        
        [ContextMenu("Test RequestUserStats")]
        private void RequestUserStatsTest() => RequestUserStats();
        public void RequestUserStats()
        {
            var coroutine = GetUserStats();
            StartCoroutine(coroutine);
        }
        private IEnumerator GetUserStats()
        {
            UnityWebRequest request = UnityWebRequest.Get(APIAddress + $"/stats?userid={StorageUtility.LoadClientGuid()}");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                UserStats user_stats = JsonConvert.DeserializeObject<UserStats>(request.downloadHandler.text);
                userStatsReceivedEvent.Invoke(user_stats);
            }
        }
    }
}

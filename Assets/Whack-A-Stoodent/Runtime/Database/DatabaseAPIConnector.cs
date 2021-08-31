using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using WhackAStoodent.Client;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Database
{
    public class DatabaseAPIConnector : MonoBehaviour
    {
         private const string apiVersion = "v1";
        [SerializeField] private string serverAddress = "https://api.bigeti.de/whackastoodentstats";
        [SerializeField] private MatchDataEvent gameEndedEvent;
        [SerializeField] private MatchDataArrayEvent matchHistoryReceivedEvent;
        [SerializeField] private UserStatsEvent userStatsReceivedEvent;
        private string APIAddress => serverAddress + "/" + apiVersion;

        private void OnEnable()
        {
            gameEndedEvent.Subscribe(HandleGameEnded);
        }
        private void OnDisable()
        {
            gameEndedEvent.Unsubscribe(HandleGameEnded);
        }

        private void HandleGameEnded(MatchData matchData)
        {
            var coroutine = PostGameResult(matchData);
            StartCoroutine(coroutine);
        }
        private IEnumerator PostGameResult(MatchData matchData)
        {
            UnityWebRequest request = UnityWebRequest.Post(APIAddress + $"?userid={StorageUtility.LoadClientGuid()}", JsonConvert.SerializeObject(matchData));
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
        }

        public void RequestMatchHistory(int maxCount = 32)
        {
            var coroutine = GetMatchHistory(maxCount);
            StartCoroutine(coroutine);
        }
        private IEnumerator GetMatchHistory(int count)
        {
            UnityWebRequest request = UnityWebRequest.Get(APIAddress + $"/history?userid={StorageUtility.LoadClientGuid()}&count={count}");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                MatchData[] match_data = JsonConvert.DeserializeObject<MatchData[]>(request.downloadHandler.text);
                matchHistoryReceivedEvent.Invoke(match_data);
            }
        }
        
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

        private IEnumerator Get_Test() 
        {
            UnityWebRequest www = UnityWebRequest.Get("https://google.com/");
            yield return www.SendWebRequest();
 
            if (www.result != UnityWebRequest.Result.Success) 
            {
                Debug.Log(www.error);
            }
            else 
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
 
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}

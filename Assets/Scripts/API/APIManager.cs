using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace API
{
    public class APIManager
    {
        private readonly Action<Dictionary<string, JengaStackData>> _onStackedDataRetrieved;

        public APIManager(MonoBehaviour gameManager, Action<Dictionary<string, JengaStackData>> handleOnStackedDataParsed)
        {
            _onStackedDataRetrieved += handleOnStackedDataParsed;
            gameManager.StartCoroutine(FetchData((response) =>
            {
                string wrappedJson = "{\"data\":" + response + "}";

                // Deserialize the JSON response
                var jengaPieceDataList = JsonUtility.FromJson<JengaPieceDataList>(wrappedJson);

                //var stacksByGrade = new Dictionary<string, JengaPieceDataList>();
                var stacksByGrade = new Dictionary<string, JengaStackData>();
                foreach (var pieceData in jengaPieceDataList.data)
                {
                    if (stacksByGrade.ContainsKey(pieceData.grade) == false)
                    {
                        stacksByGrade.Add(pieceData.grade, new JengaStackData(pieceData.grade));
                        
                    }
                    
                    stacksByGrade[pieceData.grade].PiecesData.Add(pieceData);
                }

                _onStackedDataRetrieved?.Invoke(stacksByGrade);
            }));
        }

        public IEnumerator FetchData(Action<string> result)
        {
            var apiUrl = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
            using var request = UnityWebRequest.Get(apiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                result(request.downloadHandler.text);
            }
        }
    }
}
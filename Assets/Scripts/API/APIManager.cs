using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace API
{
    public static class APIManager
    {
        // Event for when stacked data is retrieved and parsed
        private static Action<Dictionary<string, JengaStackData>> _onStackedDataRetrieved;

        // Public method to initiate the data retrieval process
        public static void RetrieveData(MonoBehaviour gameManager, Action<Dictionary<string, JengaStackData>> handleOnStackedDataParsed)
        {
            Debug.Log("Retrieving Data");
            _onStackedDataRetrieved += handleOnStackedDataParsed;
            gameManager.StartCoroutine(FetchData(ProcessData));
        }
        
        // Process the fetched data and invoke the event with the result
        private static void ProcessData(string response)
        {
            string wrappedJson = "{\"data\":" + response + "}";
            var jengaPieceDataList = JsonUtility.FromJson<JengaPieceDataList>(wrappedJson);
            var stacksByGrade = OrganizeStacksByGrade(jengaPieceDataList);
            SortStacksToSpecification(stacksByGrade);
            _onStackedDataRetrieved?.Invoke(stacksByGrade);
        }
        
        // Organize the pieces into stacks based on their grade
        private static Dictionary<string, JengaStackData> OrganizeStacksByGrade(JengaPieceDataList jengaPieceDataList)
        {
            var stacksByGrade = new Dictionary<string, JengaStackData>();
            foreach (var pieceData in jengaPieceDataList.data)
            {
                if (!stacksByGrade.ContainsKey(pieceData.grade))
                {
                    stacksByGrade.Add(pieceData.grade, new JengaStackData(pieceData.grade));
                }
                stacksByGrade[pieceData.grade].PiecesData.Add(pieceData);
            }
            return stacksByGrade;
        }

        private static void SortStacksToSpecification(Dictionary<string, JengaStackData> stacksByGrade)
        {
            foreach (var stackByGrade in stacksByGrade.Values)
            {
                stackByGrade.SortToSpecification();
            }
        }

        // Fetch data from the API
        private static IEnumerator FetchData(Action<string> result)
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
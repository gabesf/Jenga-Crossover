using System.Collections.Generic;
using API;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Building
{
    public class GameManager : MonoBehaviour
    {
        public JengaStackBuilder jengaStackBuilder;

        public static Action OnEnablePhysics;
        private void Start()
        {
            JengaLevelBuilder levelBuilder = new JengaLevelBuilder();
            APIManager apiManager = new APIManager(this, HandleOnStackedDataParsed);
            //apiManager.OnStackedDataRetrieved += HandleOnStackedDataParsed;
            
        }

        private void HandleOnStackedDataParsed(Dictionary<string, JengaStackData> jengaStacksData)
        {
            
            jengaStackBuilder.BuildStacks(jengaStacksData);
            
            OnEnablePhysics.Invoke();
           
        }


        
        
    }
}
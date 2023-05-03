using System;
using System.Collections.Generic;
using API;
using UnityEngine;
using UnityEngine.Serialization;

namespace Building
{
    public class GameManager : MonoBehaviour
    {
        public JengaStackBuilder jengaStackBuilder;
        public UiController uiController;
        public CameraSetup CameraSetup;
        public static Action OnEnablePhysics;
        public static Action OnTestStacks;
        public static Action OnTestTower;
        private void Start()
        {
            APIManager.RetrieveData(this, HandleOnStackedDataParsed);
        }

        private void OnEnable()
        {
            uiController.onTestStackButtonPressed += HandleOnTestStackButtonPressed;
        }

        private void HandleOnTestStackButtonPressed()
        {
            OnTestStacks?.Invoke();
        }

        private void HandleOnStackedDataParsed(Dictionary<string, JengaStackData> jengaStacksData)
        {
            
            jengaStackBuilder.BuildStacks(jengaStacksData, OnStackBuilt());
            OnEnablePhysics.Invoke();
        }

        private void OnStackBuilt()
        {
            
        }


        
        
    }
}
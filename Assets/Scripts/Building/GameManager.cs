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
        public static Action OnEnablePhysics;
        public static Action OnTestStacks;
        public static Action<List<Transform>> OnStacksBuilt;
        public static Action<bool> OnCameraSwitch;
        private void Start()
        {
            APIManager.RetrieveData(this, HandleOnStackedDataParsed);
        }

        private void OnEnable()
        {
            uiController.onTestStackButtonPressed += HandleOnTestStackButtonPressed;
            uiController.onChangeStackCamera += HandleNextStackButtonPressed;
        }

        private void HandleNextStackButtonPressed(bool goToPrevious)
        {
            OnCameraSwitch.Invoke(goToPrevious);
        }

        private void OnDisable()
        {
            uiController.onTestStackButtonPressed -= HandleOnTestStackButtonPressed;
        }

        private void HandleOnTestStackButtonPressed()
        {
            OnTestStacks?.Invoke();
        }

        private void HandleOnStackedDataParsed(Dictionary<string, JengaStackData> jengaStacksData)
        {
            
            var stacks = jengaStackBuilder.BuildStacks(jengaStacksData);
            OnStacksBuilt.Invoke(stacks);
            OnEnablePhysics.Invoke();
        }

        


        
        
    }
}
using System;
using System.Collections.Generic;
using API;
using Ui;
using UnityEngine;

namespace Building
{
    public class GameManager : MonoBehaviour
    {
        public JengaStackBuilder jengaStackBuilder;
        public UiController uiController;

        // Actions for events
        public static Action OnEnablePhysics;
        public static Action OnTestStacks;
        public static Action<List<Transform>> OnStacksBuilt;
        public static Action<RebuildData> OnRequestToStackRebuild;
        public static Action<bool> OnCameraSwitch;

        private void Start()
        {
            APIManager.RetrieveData(this, HandleOnStackedDataParsed);
        }

        private void OnEnable()
        {
            SubscribeToUiEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromUiEvents();
        }

        // Subscribe to UI events
        private void SubscribeToUiEvents()
        {
            uiController.onTestStackButtonPressed += HandleOnTestStackButtonPressed;
            uiController.onChangeStackCamera += HandleNextStackButtonPressed;
            uiController.onRequestToInstantaneousTowerRebuild += HandleOnRequestToTowerRebuild;
            
        }

        private void HandleOnRequestToTowerRebuild(RebuildData rebuildData)
        {
            OnRequestToStackRebuild?.Invoke(rebuildData);
        }

        // Unsubscribe from UI events
        private void UnsubscribeFromUiEvents()
        {
            uiController.onTestStackButtonPressed -= HandleOnTestStackButtonPressed;
            uiController.onChangeStackCamera -= HandleNextStackButtonPressed;
            uiController.onRequestToInstantaneousTowerRebuild -= HandleOnRequestToTowerRebuild;

        }

        // Handle the next stack button press event
        private void HandleNextStackButtonPressed(bool goToPrevious)
        {
            OnCameraSwitch.Invoke(goToPrevious);
        }

        // Handle the test stack button press event
        private void HandleOnTestStackButtonPressed()
        {
            OnTestStacks?.Invoke();
        }

        // Handle parsed stacked data and build the stacks
        private void HandleOnStackedDataParsed(Dictionary<string, JengaStackData> jengaStacksData)
        {
            var stacks = jengaStackBuilder.BuildStacks(jengaStacksData);
            OnStacksBuilt.Invoke(stacks);
            OnEnablePhysics.Invoke();
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    public class UiController : MonoBehaviour
    {
        public Button testStackButton;
        public Button earthQuakeButton;
        public Button challengeButton;
        public Button nextStackButton;
        public Button previousStackButton;
        public Button openUiPanelButton;
        public Button closeUiPanelButton;

        public Action onTestStackButtonPressed;
        public Action<bool> onChangeStackCamera;
        public Action<RebuildData> onRequestToInstantaneousTowerRebuild;

        public UiControlPanelToggle uiControlPanelToggle;
        private void Start()
        {
            testStackButton.onClick.AddListener(OnTestStackCallback);
            earthQuakeButton.onClick.AddListener(OnTowerRebuildCallback);
            challengeButton.onClick.AddListener(OnChallengeButtonCallback);
            nextStackButton.onClick.AddListener(OnShowNextStackCallback);
            previousStackButton.onClick.AddListener(OnShowPreviousStackCallback);
            openUiPanelButton.onClick.AddListener(OnOpenUiPanelUiCallback);
            closeUiPanelButton.onClick.AddListener(OnCloseUiPanelUiCallback);

        }

        private void OnChallengeButtonCallback()
        {
            onRequestToInstantaneousTowerRebuild?.Invoke(new DynamicRebuild(0.05f, 0.5f));
        }

        private void OnTowerRebuildCallback()
        {
            onRequestToInstantaneousTowerRebuild?.Invoke(new InstantaneousRebuild());
        }

        private void OnCloseUiPanelUiCallback()
        {
            uiControlPanelToggle.CloseUiPanel();    }

        private void OnOpenUiPanelUiCallback()
        {
            uiControlPanelToggle.OpenUiPanel();
        }

        private void OnTestStackCallback()
        {
            Debug.Log("Testing Stack");
            onTestStackButtonPressed?.Invoke();
        }

        private void OnShowNextStackCallback()
        {
            onChangeStackCamera?.Invoke(false);
        }

        private void OnShowPreviousStackCallback()
        {
            onChangeStackCamera?.Invoke(true);
        }
    }
}

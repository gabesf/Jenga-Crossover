using UnityEngine;
using UnityEngine.Serialization;

namespace Ui
{
    public class UiControlPanelToggler : MonoBehaviour
    {
        public Transform controlPanelAnchor;

        public GameObject openUiPanelButton;
        public GameObject closeUiPanelButton;

        public float hidePosition;
        public float openOffset;

        public float timeToShow = 0.75f;
        public LeanTweenType tweenToOpenPanel;
        public float timeToHide = 0.75f;
        public LeanTweenType tweenToClosePanel;
        private void Start()
        {

            CloseUiPanel();
        }

        public void OpenUiPanel()
        {
            LeanTween.moveLocalX(controlPanelAnchor.gameObject, openOffset, timeToShow)
                .setEase(tweenToOpenPanel)
                .setOnComplete(() =>
                {
                    DisplayClosePanelButton();

                });
        }
    
        public void CloseUiPanel()
        {
            LeanTween.moveLocalX(controlPanelAnchor.gameObject, hidePosition, timeToHide)
                .setEase(tweenToClosePanel)
                .setOnComplete(DisplayOpenPanelButton);
        }

        private void DisplayClosePanelButton()
        {
            openUiPanelButton.SetActive(false);
            closeUiPanelButton.SetActive(true);
        }

        private void DisplayOpenPanelButton()
        {
            openUiPanelButton.SetActive(true);
            closeUiPanelButton.SetActive(false);
        }
    }
}

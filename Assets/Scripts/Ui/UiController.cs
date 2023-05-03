
using System;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Button testStackButton;
    public Button earthQuakeButton;
    public Button nextStackButton;
    public Button previousStackButton;
    public Button openUiPanelButton;
    public Button closeUiPanelButton;

    public Action onTestStackButtonPressed;
    public Action<bool> onChangeStackCamera;

    public UiControlPanelToggler uiControlPanelToggler;
    private void Start()
    {
        testStackButton.onClick.AddListener(OnTestStackCallback);
        nextStackButton.onClick.AddListener(OnShowNextStackCallback);
        previousStackButton.onClick.AddListener(OnShowPreviousStackCallback);
        openUiPanelButton.onClick.AddListener(OnOpenUiPanelUiCallback);
        closeUiPanelButton.onClick.AddListener(OnCloseUiPanelUiCallback);

    }

    private void OnCloseUiPanelUiCallback()
    {
        uiControlPanelToggler.CloseUiPanel();    }

    private void OnOpenUiPanelUiCallback()
    {
        uiControlPanelToggler.OpenUiPanel();
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

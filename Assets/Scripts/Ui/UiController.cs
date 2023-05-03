
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
    public Action _onShowNextStack;
    public Action _onShowPreviousStack;

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
        _onShowNextStack?.Invoke();
    }

    private void OnShowPreviousStackCallback()
    {
        _onShowPreviousStack?.Invoke();
    }
}

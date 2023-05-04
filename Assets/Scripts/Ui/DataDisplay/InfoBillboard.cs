using System;
using API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.DataDisplay
{
    public class InfoBillboard : MonoBehaviour
    {
        [SerializeReference] private Transform cameraTransform;

        [SerializeReference] private Transform panelParentTransform;
        [SerializeReference] private Button closeButton;
        //[SerializeReference] private TextMeshPro text;
        [SerializeReference] private TextMeshProUGUI text;
        [SerializeReference] private LeanTweenType expandEasing;
        [SerializeReference] private float expandTime;
        [SerializeReference] private LeanTweenType contractEasing;
        [SerializeReference] private float contractTime;

        private bool _isVisible;

        public static Action OnInfoBillboardClosed;
        private void OnEnable()
        {
            PieceSelector.OnJengaPieceSelected += HandleOnJengaPieceSelected;
        }

        private void HandleOnJengaPieceSelected(JengaPieceData jengaPieceData)
        {
            UpdateDataText(BillBoardStringBuilder.GetTextFromPieceData(jengaPieceData));
        
            if (_isVisible == false)
            {
                ShowDataPanel();
            }
        }

        private void UpdateDataText(string newText)
        {
            text.text = newText;
            //throw new NotImplementedException();
        }

        private void Start()
        {
            closeButton.onClick.AddListener(HandleOnCloseButtonPressed);
            //if (Camera.main != null) _cameraTransform = Camera.main.transform;
        }

        private void HandleOnCloseButtonPressed()
        {
            HideDataPanel();
        }

        private void HideDataPanel()
        {
            LeanTween.scale(panelParentTransform.gameObject, Vector3.zero, contractTime)
                .setEase(contractEasing)
                .setOnComplete(() => 
                {
                    _isVisible = false;
                    OnInfoBillboardClosed.Invoke(); }
            );
        }

        private void ShowDataPanel()
        {
            LeanTween.scale(panelParentTransform.gameObject, Vector3.one, expandTime)
                .setEase(expandEasing)
                .setOnComplete(() => _isVisible = true);

        }
        
    }
}
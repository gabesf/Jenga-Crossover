using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBillboard : MonoBehaviour
{
    [SerializeReference] private Transform _cameraTransform;

    public Button closeButton;

    private void OnEnable()
    {
        PieceSelector.OnJengaPieceSelected += HandleOnJengaPieceSelected;
    }

    private void HandleOnJengaPieceSelected(JengaPieceData obj)
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        closeButton.onClick.AddListener(HandleOnCloseButtonPressed);
        //if (Camera.main != null) _cameraTransform = Camera.main.transform;
    }

    private void HandleOnCloseButtonPressed()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.LookAt(_cameraTransform.position, Vector3.up);
        transform.forward = _cameraTransform.forward;
    }
}

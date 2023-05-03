using System;
using Building;
using UnityEngine;

namespace CameraControl
{
    public class CameraController : MonoBehaviour
    {
        private readonly int _cameraCount = 3;
        private int _currentCamera;
        private CameraAnimatorStateController _cameraAnimatorStateController;

        private void Awake()
        {
            _cameraAnimatorStateController = GetComponent<CameraAnimatorStateController>();
        }

        public void OnEnable()
        {
            GameManager.OnCameraSwitch += HandleOnCameraSwitch;
        }

        private void HandleOnCameraSwitch(bool goToPreviousStack)
        {
            _currentCamera = goToPreviousStack == true ? _currentCamera - 1 : _currentCamera + 1;
            _currentCamera = _currentCamera < 0 ? _cameraCount - 1 : _currentCamera;
            _currentCamera = _currentCamera > _cameraCount - 1 ? 0 : _currentCamera;
            _cameraAnimatorStateController.SetAnimationState(_currentCamera);
            Debug.Log($"Switching camera to {_currentCamera}");


        }
    }
}

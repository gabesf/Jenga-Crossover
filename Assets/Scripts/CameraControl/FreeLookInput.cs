using Cinemachine;
using UnityEngine;

public class FreeLookInput : MonoBehaviour
{
    private CinemachineFreeLook _freeLookCamera;

    private string _xAxisName = "Mouse X";
    private string _yAxisName = "Mouse Y";

    private void Start()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();
        _freeLookCamera.m_XAxis.m_InputAxisName = "";
        _freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _freeLookCamera.m_XAxis.m_InputAxisValue = Input.GetAxis((_xAxisName));
            _freeLookCamera.m_YAxis.m_InputAxisValue = Input.GetAxis((_yAxisName));
        }
        else
        {
            _freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
            _freeLookCamera.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}

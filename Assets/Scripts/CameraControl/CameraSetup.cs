using System;
using System.Collections;
using System.Collections.Generic;
using Building;
using Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public CinemachineFreeLook[] cameras;
    private void Awake()
    {
        GameManager.OnStacksBuilt += HandleOnStacksBuilt;
        cameras = GetComponentsInChildren<CinemachineFreeLook>();
    }

    private void HandleOnStacksBuilt(List<Transform> stacks)
    {
        for (int i = 0; i < stacks.Count; i++)
        {
            var stack = stacks[i];
            cameras[i].m_Follow = stack;
            cameras[i].m_LookAt = stack;
        }
    }
}

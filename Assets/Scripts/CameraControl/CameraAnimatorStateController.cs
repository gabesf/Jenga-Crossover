using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimatorStateController : MonoBehaviour
{
    private Animator targetAnimator;

    private void Start()
    {
        targetAnimator = GetComponent<Animator>();
    }

    public void SetAnimationState(string stateName)
    {
        targetAnimator.CrossFade(stateName, 0f);
    }
}

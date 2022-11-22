using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamerasHolder : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineFreeLook cineMachineFreeLook;

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
        cineMachineFreeLook = GetComponentInChildren<CinemachineFreeLook>();
    }
}

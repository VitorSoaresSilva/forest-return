using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ForestReturn.Scripts.PlayerAction.Managers;
using UnityEngine;

public class FreeLookCameraConfig : MonoBehaviour
{
    public CinemachineFreeLook CinemachineFreeLook;

    private void Start()
    {
        if (LevelManager.instance != null)
        {
            var playerScriptTransform = LevelManager.instance.PlayerScript.transform;
            CinemachineFreeLook.Follow = playerScriptTransform;
            CinemachineFreeLook.LookAt = playerScriptTransform;
        }
    }
}

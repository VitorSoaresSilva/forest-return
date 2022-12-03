using System;
using Cinemachine;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.Cameras
{
    public class FreeLookCameraConfig : MonoBehaviour
    {
        public CinemachineFreeLook CinemachineFreeLook;

        private void Start()
        {
            if (LevelManager.Instance != null)
            {
                var playerScriptTransform = LevelManager.Instance.PlayerScript.transform;
                CinemachineFreeLook.Follow = playerScriptTransform;
                CinemachineFreeLook.LookAt = playerScriptTransform;
            }
            var mYAxis = CinemachineFreeLook.m_YAxis;
            mYAxis.Value = 0.5f;
            CinemachineFreeLook.m_YAxis = mYAxis;
        }


    }
}

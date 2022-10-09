using Cinemachine;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Camera
{
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

            var mYAxis = CinemachineFreeLook.m_YAxis;
            mYAxis.Value = 0.5f;
            CinemachineFreeLook.m_YAxis = mYAxis;
        }
    }
}

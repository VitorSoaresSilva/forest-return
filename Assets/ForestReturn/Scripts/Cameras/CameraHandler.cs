using System;
using Cinemachine;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.PlayerScripts;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Cameras
{
    public class CameraHandler : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private CinemachineFreeLook _cineMachine;

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
        }

        public void Init()
        {
            if (LevelManager.InstanceExists)
            {
                _cineMachine = LevelManager.Instance.CamerasHolder.cineMachineFreeLook;
            }
        }

        public void HandleCameraRotation(float delta)
        {
            // _cineMachine.m_YAxis.Value += _inputHandler.mouseY * delta * _cineMachine.m_YAxis.m_MaxSpeed;
            _cineMachine.m_XAxis.Value += _inputHandler.mouseX * delta * _cineMachine.m_XAxis.m_MaxSpeed;
        }

        public void HandleCameraZoom(float delta)
        {
            _cineMachine.m_YAxis.Value += _inputHandler.mouseY * delta * _cineMachine.m_YAxis.m_MaxSpeed;
        }
    }
}
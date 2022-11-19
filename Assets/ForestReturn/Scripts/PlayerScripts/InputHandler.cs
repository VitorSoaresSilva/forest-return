using System;
using ForestReturn.Scripts.Cameras;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        public bool bInput;
        public bool rollFlag;
        public bool isInteracting;
        
        private PlayerInputAction _inputActions;
        private CameraHandler _cameraHandler; 
        
        private Vector2 _movementInput;
        private Vector2 _cameraInput;

        private void Awake()
        {
            _cameraHandler = CameraHandler.Instance;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (_cameraHandler != null)
            {
                _cameraHandler.FollowTarget(delta);
                _cameraHandler.HandleCameraRotation(delta,mouseX,mouseY);
            }
        }

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputAction();
                _inputActions.gameplay.move.performed += i => _movementInput = i.ReadValue<Vector2>();
                _inputActions.gameplay.Camera.performed += i => _cameraInput = i.ReadValue<Vector2>();
            }
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = _movementInput.x;
            vertical = _movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = _cameraInput.x;
            mouseY = _cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            bInput = _inputActions.gameplay.Roll.phase == InputActionPhase.Performed;
            if (bInput)
            {
                rollFlag = true;
            }
        }
    }
}
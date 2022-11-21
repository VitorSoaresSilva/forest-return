using System;
using ForestReturn.Scripts.Cameras;
using ForestReturn.Scripts.Managers;
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
        
        
        private PlayerInputAction _inputActions;
        private PlayerAttacker _playerAttacker;
        private PlayerManager _playerManager;
        private PlayerInteractableHandler _playerInteractableHandler;
        
        private Vector2 _movementInput;
        private Vector2 _cameraInput;

        private void Awake()
        {
            _playerAttacker = GetComponent<PlayerAttacker>();
            _playerManager = GetComponent<PlayerManager>();
            _playerInteractableHandler = GetComponentInChildren<PlayerInteractableHandler>();
        }

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputAction();
                _inputActions.gameplay.move.performed += i => _movementInput = i.ReadValue<Vector2>();
                _inputActions.gameplay.Camera.performed += i => _cameraInput = i.ReadValue<Vector2>();
                _inputActions.gameplay.Attack.performed += i => _playerAttacker.HandleLightAttack();
                _inputActions.gameplay.Interact.performed += i => HandleInteraction();
                _inputActions.gameplay.inventory.performed += i => HandleInventory();
                _inputActions.gameplay.VinesSkill.performed += i => _playerManager.OnVinesSkill();
                
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
        private void HandleInteraction()
        {
            if (_playerManager.isInteracting) return;
            _playerInteractableHandler.CurrentInteractable?.Interactable?.Interact();
            _playerInteractableHandler.Reset();
        }

        private void HandleInventory()
        {
            if (_playerManager.isInteracting) return;
            GameManager.Instance.PauseGame();
            UiManager.Instance.OpenCanvas(CanvasType.Menu); /*troca inventário - menu*/
        }

        
    }
}
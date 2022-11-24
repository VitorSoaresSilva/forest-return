using System;
using ForestReturn.Scripts.Cameras;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.UI.TabSystem;
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
                _inputActions.gameplay.Move.performed += i => _movementInput = i.ReadValue<Vector2>();
                _inputActions.gameplay.Attack.performed += i => _playerAttacker.HandleLightAttack();
                _inputActions.gameplay.RangeAttack.performed += i => _playerAttacker.HandleRangedAttack();
                _inputActions.gameplay.Interact.performed += i => HandleInteraction();
                _inputActions.gameplay.Pause.performed += i => HandlePause();
                _inputActions.gameplay.Inventory.performed += i => HandleInventory();
                _inputActions.gameplay.LifePotion.performed += i => _playerManager.HandleLifePotion();
                _inputActions.gameplay.ManaPotion.performed += i => _playerManager.HandleManaPotion();
                _inputActions.gameplay.Teleport.performed += i => _playerManager.HandleTeleport();
                // _inputActions.gameplay.Defense.performed += i => _playerManager.HandleTeleport();
                _inputActions.gameplay.VinesSkill.performed += i => _playerManager.OnVinesSkill();
                _inputActions.gameplay.Camera.performed += i => _cameraInput = i.ReadValue<Vector2>();
                _inputActions.gameplay.Roll.performed += i => rollFlag = true;
                
                
                
                _inputActions.Menu.Exit.performed += i => _playerManager.HandleResume();
                _inputActions.Menu.Move.performed += i => Debug.Log("Move");
                _inputActions.Menu.ChangeTab.performed += i => HandleChangeTab(i.ReadValue<float>());
                _inputActions.UI.Cancel.performed += i => Debug.Log("Cancel");
                
                
                
                
                
                // _inputActions.Menu.Exit.performed += i => HandlePause();

            }
            SwitchActionMap("gameplay");
            // _inputActions.gameplay.Enable();
            // _inputActions.UI.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            // _inputActions.gameplay.move.performed -= Move;
            // _inputActions.gameplay.Camera.performed -=null;
            // _inputActions.gameplay.Attack.performed -=null;
            // _inputActions.gameplay.Interact.performed -=null;
            // _inputActions.gameplay.inventory.performed -=null;
            // _inputActions.gameplay.VinesSkill.performed -=null;
            // _inputActions.gameplay.Roll.performed -=null;
            // _inputActions.gameplay.pause.performed -=null;
            // _inputActions.gameplay.LifePotion.performed -=null;
            // _inputActions.gameplay.ManaPotion.performed -=null; 
            // _inputActions.Menu.Exit.performed -=null;
            // _inputActions.Menu.ChangeTab.performed -=null;
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            // HandleRollInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = _movementInput.x;
            vertical = _movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = _cameraInput.x;
            mouseY = _cameraInput.y;
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
            if (!GameManager.InstanceExists) return;
            UiManager.Instance.OpenCanvas(CanvasType.Menu); /*troca inventário - menu*/
            GameManager.Instance.PauseGame();
        }
        private void HandlePause()
        {
            if (_playerManager.isInteracting) return;
            if (!GameManager.InstanceExists) return;
            if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            else
            {
                GameManager.Instance.PauseGame();
                UiManager.Instance.OpenCanvas(CanvasType.Pause);
            }
        }

        private void HandleChangeTab(float value)
        {
            if (!TabGroup.InstanceExists) return;
            TabGroup.Instance.ChangeTab((int)value);
        }

        public void SwitchActionMap(string actionType)
        {
            _inputActions.UI.Disable();
            _inputActions.gameplay.Disable();
            _inputActions.Death.Disable();
            _inputActions.Pause.Disable();
            _inputActions.Menu.Disable();
            switch (actionType)
            {
                case "gameplay":
                    _inputActions.gameplay.Enable();
                    break;
                case "UI":
                    // _inputActions.UI.Enable();
                    break;
            }
            // _inputActions.UI.
        }
        
    }
}
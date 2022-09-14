using System;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Inventory;
using Interactable;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ForestReturn.Scripts.PlayerAction
{
    public class Player : BaseCharacter
    {
        private CharacterController _controller;
        private Animator _animator;
        [Header("Movement")]
        [SerializeField] private float speed; 
        [SerializeField] private float turnSmoothTime = 0.1f;
        private Vector2 _move; 
        private Vector2 _look;
        public Transform cam;
        
        [Header("Interact")]
        [SerializeField] private Vector3 offsetInteract;
        [SerializeField] private float sphereInteractionRadius;
        private readonly RaycastHit[] _raycastHits = new RaycastHit[3];
        private bool _isAttacking;
        private bool _isDashing;
        private float _turnSmoothVelocity;

        [Header("Damage")] 
        [SerializeField] private GameObject swordHitBox;

        private PlayerInput _playerInput;
        private InventoryObject _inventoryObject;
        
        // Animations
        private static readonly int AttackPunch = Animator.StringToHash("Attack");
        private static readonly int RangedAttack = Animator.StringToHash("RangedAttack");
        private static readonly int Walking = Animator.StringToHash("isMoving");
        [SerializeField] private LayerMask itemsLayer;
        
        
        
        // parameters
        [SerializeField] private ParameterObject more10PercentNormalAttack;
        
        // Damage
        public DataDamage DataDamage
        {
            get
            {
                return new DataDamage(1);
            }
            
        }

        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _inventoryObject = InventoryManager.instance.inventory;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Move();
            _animator.SetBool(Walking,_move.sqrMagnitude > 0.01f);
        }

        private void OnEnable()
        {
            OnDead += HandleDeath;
            OnHurt += HandleHurt;
            OnManaHealed += HandleManaHealed;
            OnHealthHealed += HandleHealthHealed;
        }

        private void OnDisable()
        {
            OnDead -= HandleDeath;
            OnHurt -= HandleHurt;
            OnManaHealed -= HandleManaHealed;
            OnHealthHealed -= HandleHealthHealed;
        }

        private void Move()
        {
            if (_move.sqrMagnitude < 0.01) //  || _isAttacking
                return;
            float targetAngle = Mathf.Atan2(_move.x,_move.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDirection.normalized * (speed * Time.deltaTime));
        }



        #region Gameplay
        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (_isAttacking) return;
            _isAttacking = true;
            _animator.SetTrigger(AttackPunch);
        }
        public void OnRangeAttack(InputAction.CallbackContext context)
        {
            if (_isAttacking) return;
            _isAttacking = true;
            _animator.SetTrigger(RangedAttack);
        }
        
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var raycastHits = Physics.SphereCastAll(transform.position,sphereInteractionRadius,transform.forward,1,itemsLayer);
            if (raycastHits.Length <= 0) return;
            int closestIndex = 0;
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].distance < raycastHits[closestIndex].distance)
                {
                    closestIndex = i;
                }
            }
            raycastHits[closestIndex].transform.TryGetComponent(out IInteractable closestInteractable);
            closestInteractable?.Interact();
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            _playerInput.SwitchCurrentActionMap("Inventory");
            InventoryManager.instance.OpenInventory();
        }

        public void OnLifePotion(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!(CurrentHealth < MaxHealth)) return;
            var potions = _inventoryObject.GetPotionByType(PotionType.Life);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                HealthHeal(potion.value); 
                return;
            }

            Debug.Log("Out of Life's Potion");
            // TODO: Show in UI 'out of life's potion'
        }
        public void OnManaPotion(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!(CurrentMana < MaxMana)) return;
            var potions = _inventoryObject.GetPotionByType(PotionType.Mana);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                ManaHeal(potion.value);
                return;
            }

            Debug.Log("Out of Mana's Potion");
            // TODO: Show in UI 'out of mana's potion'
        }
        #endregion

        #region Inventory

        public void OnInventoryMove(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var direction = context.ReadValue<Vector2>();
            Debug.Log(direction);
        }

        public void OnInventoryClose(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            InventoryManager.instance.CloseInventory();
            _playerInput.SwitchCurrentActionMap("gameplay");
        }

        #endregion

        #region Handles
        public void HandleEndAttack()
        {
            _isAttacking = false;
            swordHitBox.SetActive(false);
        }

        public void HandleStartAttack()
        {
            swordHitBox.SetActive(true);
        }

        public void HandleStartRangedAttack()
        {
            // _isAttacking = true;
            // neste ponto vou castar o espinho e diminuir na quantidade de espinhos disponiveis
        } 

        public void HandleEndRangedAttack()
        {
            _isAttacking = false;
        } 
        private void HandleDeath()
        {
            // _playerInput.SwitchCurrentActionMap("deathScreen");
        }
        private void HandleHurt()
        {
            if (UiManager.instance != null)
            {
                UiManager.instance.PlayerHurt();
            }
        }
        private void HandleHealthHealed()
        {
            Debug.Log("Life healed");
        }

        private void HandleManaHealed()
        {
            Debug.Log("Mana healed");
        }

        #endregion
    }
}
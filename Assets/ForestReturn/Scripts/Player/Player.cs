using System;
using _Developers.Vitor.Scripts.Interactable;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.UI.TabSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Enums = ForestReturn.Scripts.Utilities.Enums;

namespace ForestReturn.Scripts
{
    public class Player : BaseCharacter
    {
        private CharacterController _controller;
        private Animator _animator;
        private bool _isAttacking;
        private bool _isDashing;
        private float _turnSmoothVelocity;
        private PlayerInput _playerInput;
        private InventoryObject _inventoryObjectRef;
        
        [Header("Movement")]
        private float _currentSpeed; 
        [SerializeField] private float normalSpeed; 
        // [SerializeField] private float attackForwardStepSpeed;
        [SerializeField] private float turnSmoothTime = 0.1f;
        private bool _isMovingForwardByAttack;
        private Vector2 _move; 
        private Vector2 _look;
        private Transform _cam;
        
        [Header("Interact")]
        [SerializeField] private GameObject sphereCollider;
        // [SerializeField] private Vector3 offsetInteract;
        // [SerializeField] private float sphereInteractionRadius;
        // private readonly RaycastHit[] _raycastHits = new RaycastHit[3];

        [Header("Damage")] 
        [SerializeField] private GameObject swordHitBox;
        [SerializeField] private PlayerAttack[] attacks;

        
        // Animations
        private static readonly int AttackPunchHashAnimation = Animator.StringToHash("Attack");
        private static readonly int AttackPunchBackHashAnimation = Animator.StringToHash("AttackBack");
        private static readonly int RangedAttackHashAnimation = Animator.StringToHash("RangedAttack");
        private static readonly int WalkingHashAnimation = Animator.StringToHash("isMoving");
        private static readonly int DeathHashAnimation = Animator.StringToHash("Death");
        // [SerializeField] private LayerMask itemsLayer;

        // public WeaponObject currentWeapon;
        public ParticleSystem[] _particleSystemsTeleport;

        [Header("Attack")] 
        private bool acceptComboAttack;
        [SerializeField] private GameObject swordEffect;
        private static readonly int IsDefendingHashAnimation = Animator.StringToHash("IsDefending");

        public void Init()
        {
            if (InventoryManager.instance != null)
            {
                _inventoryObjectRef = InventoryManager.instance.inventory;
            }
            if (LevelManager.instance != null)
            {
                _controller.enabled = false;
                transform.position = LevelManager.instance.pointToSpawn;
                _controller.enabled = true;
            }

            _currentSpeed = normalSpeed;
            if (Camera.main != null) _cam = Camera.main.transform;
        }
        
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
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            // if (_isMovingForwardByAttack)
            // {
            //     _controller.Move(transform.forward * (attackForwardStepSpeed * Time.deltaTime));
            // }
            // if (!_isAttacking)
            // {
            // }
                Move();
            _controller.Move(Vector3.down * (-Physics.gravity.y * Time.deltaTime)); // Add Gravity
            _animator.SetBool(WalkingHashAnimation,_move.sqrMagnitude > 0.01f);
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
            float targetAngle = Mathf.Atan2(_move.x,_move.y) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDirection.normalized * (_currentSpeed * Time.deltaTime));
        }



        #region Gameplay
        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {

            if (!context.performed) return;


            if (!_isAttacking)
            {
                _isAttacking = true;
                _animator.SetTrigger(AttackPunchHashAnimation);
            }
            else if (acceptComboAttack)
            {
                acceptComboAttack = false;
                swordHitBox.SetActive(false);
                _animator.SetTrigger(AttackPunchBackHashAnimation);
            }
            
            
            
            // if (!_isAttacking)
            // {
            //     _isAttacking = true;
            //     _animator.SetTrigger(AttackPunch);
            // }
            // else if (acceptComboAttack)
            // {
            //     swordHitBox.SetActive(true);
            //     _animator.SetTrigger(AttackPunchBack); 
            // }


            // if (_isAttacking ) return;
            // _isAttacking = true;
            // _animator.SetTrigger(AttackPunch);
        }
        public void OnRangeAttack(InputAction.CallbackContext context)
        {
            if (_isAttacking) return;
            _isAttacking = true;
            _animator.SetTrigger(RangedAttackHashAnimation);
        }
        
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            sphereCollider.SetActive(true);
            /*var raycastHits = Physics.SphereCastAll(transform.position,sphereInteractionRadius,transform.forward,1,itemsLayer);
            
            if (raycastHits.Length <= 0) return;
            int closestIndex = 0;
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].distance < raycastHits[closestIndex].distance)
                {
                    closestIndex = i;
                }
            }
            raycastHits[closestIndex].transform.TryGetComponent(out IInteractable closestInteractable);*/
            //tInteractable?.Interact();
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            _playerInput.SwitchCurrentActionMap("Menu");
            UiManager.instance.OpenCanvas(CanvasType.Menu); /*troca inventário - menu*/
        }

        public void OnLifePotion(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!(CurrentHealth < MaxHealth)) return;
            var potions = _inventoryObjectRef.GetPotionByType(PotionType.Life);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                InventoryManager.instance.inventory.RemoveItem(potion);
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
            var potions = _inventoryObjectRef.GetPotionByType(PotionType.Mana);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                InventoryManager.instance.inventory.RemoveItem(potion);
                ManaHeal(potion.value);
                return;
            }

            Debug.Log("Out of Mana's Potion");
            // TODO: Show in UI 'out of mana's potion'
        }

        public void OnTeleport(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (GameManager.instance.generalData.currentLevel == Enums.Scenes.Lobby && 
                GameManager.instance.generalData.TeleportData is { AlreadyReturned: false })
            {
                foreach (var particle in _particleSystemsTeleport)
                {
                    particle.Play();
                }
                GameManager.instance.HandleTeleport(null);
                _playerInput.enabled = false;
            }

            
            var teleportItems = InventoryManager.instance.inventory.GetItemsByType(ItemType.Teleport);
            if (GameManager.instance.generalData.currentLevel != Enums.Scenes.Lobby &&
                teleportItems.Count > 0)
            {
                foreach (var particle in _particleSystemsTeleport)
                {
                    particle.Play();
                }
                var teleportItem = teleportItems[0].item;
                InventoryManager.instance.inventory.RemoveItem(teleportItem);
                GameManager.instance.HandleTeleport(new TeleportData(transform.position, Enums.Scenes.Level01));
                _playerInput.enabled = false;
            }
        }

        public void OnDefense(InputAction.CallbackContext context)
        {
            Debug.Log(context.phase);

            if (context.performed)
            {
                _animator.SetBool(IsDefendingHashAnimation, true);
                _currentSpeed = 0;
                IsDefending = true;
                // defense += value
            }
            else if (context.canceled)
            {
                _animator.SetBool(IsDefendingHashAnimation, false);
                _currentSpeed = normalSpeed;
                IsDefending = false;
                // defense -= value
            }
            // if (context.performed)
            // {
            //     
            // }
            // if()
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
            UiManager.instance.OpenCanvas(CanvasType.Hud);
            _playerInput.SwitchCurrentActionMap("gameplay");
        }
        public void OnChangeTabs(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var a = context.valueType;
            var b = context.ReadValue<float>();
            TabGroup.instance.ChangeTab((int)b);
        }

        #endregion

        #region Handles

        public void EnableHitBox()
        {
            swordHitBox.SetActive(true);
        }

        public void DisableHitBox()
        {
            swordHitBox.SetActive(false);
        }
        
        // public void HandleStartAttack()
        // {
        //     acceptComboAttack = false;
        //     swordHitBox.SetActive(true);
        //     if (!swordEffect.activeSelf)
        //     {
        //         swordEffect.SetActive(true);
        //     }
        // }
        // public void HandleEndAttack()
        // {
        //     _isAttacking = false;
        //     acceptComboAttack = false;
        //     swordHitBox.SetActive(false);
        // }


        // public void HandleStartRangedAttack()
        // {
        //     // _isAttacking = true;
        //     // neste ponto vou castar o espinho e diminuir na quantidade de espinhos disponiveis
        // } 

        // public void HandleEndRangedAttack()
        // {
        //     _isAttacking = false;
        // }
        //
        public void HandleStartRangeSecondAttack()
        {
            acceptComboAttack = true;
        }
        //
        public void HandleEndRangeSecondAttack()
        {
            acceptComboAttack = false;
        }
        //
        // public void HandleStartMoveForward()
        // {
        //     _isMovingForwardByAttack = true;
        // }
        //
        // public void HandleEndMoveForward()
        // {
        //     _isMovingForwardByAttack = false;
        // }
        
        
        private void HandleDeath()
        {
            _playerInput.enabled = false;
            _animator.SetTrigger(DeathHashAnimation);
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

        public void HandleEndComboAttack()
        {
            _isAttacking = false;
            swordHitBox.SetActive(false);
            swordEffect.SetActive(false);
        }
    }
    [Serializable]
    public struct PlayerAttack
    {
        public int damage;
        public string animationTrigger;
        public GameObject hitBox;
    }
}
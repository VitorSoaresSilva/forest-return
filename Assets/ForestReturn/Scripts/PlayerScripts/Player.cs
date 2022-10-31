using System;
using Cinemachine;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.UI.TabSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Enums = ForestReturn.Scripts.Utilities.Enums;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class Player : BaseCharacter
    {
        private CharacterController _controller;
        private Animator _animator;
        private bool _isAttacking;
        private bool _isDashing;
        private float _turnSmoothVelocity;
        [HideInInspector] public PlayerInput _playerInput;
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
        private CinemachineFreeLook _cinemachine;

        [Header("Interact")] 
        [SerializeField] private PlayerInteractableHandler playerInteractableHandler;

        [Header("Damage")] 
        [SerializeField] private GameObject swordHitBox;
        [SerializeField] private PlayerAttack[] attacks;

        
        // Animations
        private static readonly int AttackPunchHashAnimation = Animator.StringToHash("Attack");
        private static readonly int AttackPunchBackHashAnimation = Animator.StringToHash("AttackBack");
        private static readonly int RangedAttackHashAnimation = Animator.StringToHash("RangedAttack");
        private static readonly int WalkingHashAnimation = Animator.StringToHash("isMoving");
        private static readonly int DeathHashAnimation = Animator.StringToHash("Death");
        private static readonly int IsDefendingHashAnimation = Animator.StringToHash("IsDefending");
        // [SerializeField] private LayerMask itemsLayer;

        // public WeaponObject currentWeapon;
        public ParticleSystem[] _particleSystemsTeleport;

        [Header("Attack")] 
        private bool acceptComboAttack;
        [SerializeField] private GameObject swordEffect;

        [Header("Skills")] 
        [SerializeField] private GameObject vinesSkillPrefab;
        

        public void Init()
        {
            if (InventoryManager.Instance != null)
            {
                _inventoryObjectRef = InventoryManager.Instance.inventory;
            }
            if (LevelManager.Instance != null)
            {
                _controller.enabled = false;
                transform.position = LevelManager.Instance.pointToSpawn;
                _controller.enabled = true;
            }

            if (GameManager.InstanceExists && GameManager.Instance.generalData.playerCharacterData != null)
            {
                CurrentHealth = (int)GameManager.Instance.generalData.playerCharacterData?.CurrentHealth;
                CurrentMana = (int)GameManager.Instance.generalData.playerCharacterData?.CurrentMana;
            }
            
            //equipamentos
            _currentSpeed = normalSpeed;
            UpdateAttacks();
            if (Camera.main != null) _cam = Camera.main.transform;
            _cinemachine = _cam.transform.root.GetComponentInChildren<CinemachineFreeLook>();
        }

        public void UpdateAttacks()
        {
            foreach (PlayerAttack playerAttack in attacks)
            {
                playerAttack.hitBox.TryGetComponent(out HitBox hitBox);
                if (hitBox == null)
                {
                    hitBox = playerAttack.hitBox.AddComponent<HitBox>();
                }
                hitBox.damage = Damage;
            }
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
                if (!swordEffect.activeSelf)
                {
                    swordEffect.SetActive(true);
                }
                _animator.SetTrigger(AttackPunchHashAnimation);
            }
            else if (acceptComboAttack)
            {
                acceptComboAttack = false;
                swordHitBox.SetActive(false);
                _animator.SetTrigger(AttackPunchBackHashAnimation);
            }
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (GameManager.Instance.isPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            else
            {
                GameManager.Instance.PauseGame();
                _playerInput.SwitchCurrentActionMap("Pause");
                UiManager.Instance.OpenCanvas(CanvasType.Pause);
            }
            
        }

        public void OnRangeAttack(InputAction.CallbackContext context)
        {
            if (_isAttacking) return;
            // _isAttacking = true;
            // _animator.SetTrigger(RangedAttackHashAnimation);
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            playerInteractableHandler.CurrentInteractable?.Interactable?.Interact();
            playerInteractableHandler.Reset();
        }
        public void OnMouseZoom(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var value = context.ReadValue<Vector2>();
            _cinemachine.m_YAxis.Value += value.y * Time.deltaTime * _cinemachine.m_YAxis.m_MaxSpeed;
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            GameManager.Instance.PauseGame();
            _playerInput.SwitchCurrentActionMap("Menu");
            UiManager.Instance.OpenCanvas(CanvasType.Menu); /*troca inventário - menu*/
        }

        public void OnLifePotion(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!(CurrentHealth < MaxHealth)) return;
            var potions = _inventoryObjectRef.GetPotionByType(PotionType.Life);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                InventoryManager.Instance.inventory.RemoveItem(potion);
                HealthHeal(potion.value); 
                return;
            }

            // Debug.Log("Out of Life's Potion");
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
                InventoryManager.Instance.inventory.RemoveItem(potion);
                ManaHeal(potion.value);
                return;
            }

            // Debug.Log("Out of Mana's Potion");
            // TODO: Show in UI 'out of mana's potion'
        }

        public void OnTeleport(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Debug.Log(GameManager.Instance.generalData.TeleportData != null);
            if (GameManager.Instance.generalData.TeleportData != null)
            {
                Debug.Log("not equals null");
                _playerInput.enabled = false;
                IsIntangible = true;
                GameManager.Instance.HandleTeleport(null);
            }
            else
            {
                Debug.Log("equals null");
                var teleportItems = InventoryManager.Instance.inventory.GetItemsByType(ItemType.Teleport);
                if (GameManager.Instance.generalData.currentLevel != Enums.Scenes.Lobby &&
                    teleportItems.Count > 0)
                {
                    foreach (var particle in _particleSystemsTeleport)
                    {
                        particle.Play();
                    }
                    var teleportItem = teleportItems[0].item;
                    InventoryManager.Instance.inventory.RemoveItem(teleportItem);
                    GameManager.Instance.HandleTeleport(new TeleportData(transform.position, LevelManager.Instance.sceneIndex));
                    _playerInput.enabled = false;
                }
            }
            
            // if (GameManager.Instance.generalData.currentLevel == Enums.Scenes.Lobby && 
            //     GameManager.Instance.generalData.TeleportData is { AlreadyReturned: false })
            // {
            //     foreach (var particle in _particleSystemsTeleport)
            //     {
            //         particle.Play();
            //     }
            //
            //     _playerInput.enabled = false;
            //     IsIntangible = true;
            //     GameManager.Instance.HandleTeleport(null);
            //     return;
            // }
            //
            //
            // var teleportItems = InventoryManager.Instance.inventory.GetItemsByType(ItemType.Teleport);
            // if (GameManager.Instance.generalData.currentLevel != Enums.Scenes.Lobby &&
            //     teleportItems.Count > 0)
            // {
            //     foreach (var particle in _particleSystemsTeleport)
            //     {
            //         particle.Play();
            //     }
            //     var teleportItem = teleportItems[0].item;
            //     InventoryManager.Instance.inventory.RemoveItem(teleportItem);
            //     GameManager.Instance.HandleTeleport(new TeleportData(transform.position, LevelManager.Instance.sceneIndex));
            //     _playerInput.enabled = false;
            // }
        }

        public void OnVinesSkill(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Instantiate(vinesSkillPrefab, transform.position, transform.rotation);
            }
        }
        public void OnDefense(InputAction.CallbackContext context)
        {
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
        }
        
        #endregion

        #region Inventory

        public void OnInventoryMove(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var direction = context.ReadValue<Vector2>();
            // Debug.Log(direction);
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (GameManager.Instance.isPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
            _playerInput.SwitchCurrentActionMap("gameplay");
        }
        
        public void OnChangeTabs(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var a = context.valueType;
            var b = context.ReadValue<float>();
            TabGroup.Instance.ChangeTab((int)b);
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
            // _playerInput.enabled = false;
            _animator.SetTrigger(DeathHashAnimation);
            _playerInput.SwitchCurrentActionMap("Death");
        }
        private void HandleHurt()
        {
        }
        private void HandleHealthHealed()
        {
            // Debug.Log("Life healed");
        }

        private void HandleManaHealed()
        {
            // Debug.Log("Mana healed");
        }
        
        

        #endregion

        public void HandleEndComboAttack()
        {
            _isAttacking = false;
            swordEffect.SetActive(false);
            swordHitBox.SetActive(false);
            swordEffect.SetActive(false);
        }
    }
    [Serializable]
    public struct PlayerAttack
    {
        public GameObject hitBox;
    }
}
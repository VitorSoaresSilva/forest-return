using System;
using System.Collections;
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
        [HideInInspector] public PlayerInput playerInput;
        private InventoryObject _inventoryObjectRef;

        [Header("Movement")]
        private float _currentSpeed; 
        [SerializeField] private float normalSpeed; 
        [SerializeField] private float turnSmoothTime = 0.1f;
        private Transform _cam;
        private CinemachineFreeLook _cineMachine;

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

        // private float timeToCastVineSkill;
        private float delayTimeVineSkill = 3;




        
        

        public delegate void OnVineSkillCoolDownChangedEvent(float value);
        public event OnVineSkillCoolDownChangedEvent OnVineSkillCoolDownChanged;

        private float _cooldownVinesSkillValue;

        private float CooldownValue
        {
            get => _cooldownVinesSkillValue;
            set
            {
                _cooldownVinesSkillValue = value;
                OnVineSkillCoolDownChanged?.Invoke(value);
            }
        }


        public void Init()
        {
            _cam = LevelManager.Instance.CamerasHolder.mainCamera.transform;
            _cineMachine = LevelManager.Instance.CamerasHolder.cineMachineFreeLook;
            if (InventoryManager.InstanceExists)
            {
                _inventoryObjectRef = InventoryManager.Instance.inventory;
            }
            if (LevelManager.InstanceExists)
            {
                _controller.enabled = false;
                transform.position = LevelManager.Instance.pointToSpawn;
                _controller.enabled = true;
                // _cam = LevelManager.Instance.CamerasHolder.mainCamera.transform;
                // _cinemachine = LevelManager.Instance.CamerasHolder.cineMachineFreeLook;
            }

            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnPauseGame += OnPauseGame;
                GameManager.Instance.OnResumeGame += OnResumeGame;
                if (GameManager.Instance.generalData.HasPlayerData)
                {
                    CurrentHealth = GameManager.Instance.generalData.PlayerCurrentHealth;
                    CurrentMana = GameManager.Instance.generalData.PlayerCurrentMana;
                    GameManager.Instance.generalData.ClearPlayerData();
                    
                }
                GameManager.Instance.Save();

                InitSkill();
            }

            
            
            //equipamentos
            _currentSpeed = normalSpeed;
            UpdateAttacks();
            // if (Camera.main != null) _cam = Camera.main.transform;
            // _cinemachine = _cam.transform.root.GetComponentInChildren<CinemachineFreeLook>();
        }

        private void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnResumeGame -= OnResumeGame;
                GameManager.Instance.OnPauseGame -= OnPauseGame;
            }
        }
        private void InitSkill()
        {
            CooldownValue = 1;
        }

        private void UpdateAttacks()
        {
            foreach (PlayerAttack playerAttack in attacks)
            {
                playerAttack.hitBox.TryGetComponent(out HitBox hitBox);
                if (hitBox == null)
                {
                    hitBox = playerAttack.hitBox.AddComponent<HitBox>();
                }
                hitBox.damage = CalculateDamagePerWeaponLevel();
            }
        }

        private int CalculateDamagePerWeaponLevel()
        {
            return Damage + InventoryManager.Instance.equippedItems.swordInventorySlot.level * 2;
        }
        
        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            playerInput = GetComponent<PlayerInput>();
        }
        

        private void Update()
        {
            Move();
            // _controller.Move(Vector3.down * (-Physics.gravity.y * Time.deltaTime)); // Add Gravity
            // _animator.SetBool(WalkingHashAnimation,_move.sqrMagnitude > 0.01f);
        }

        private void Move()
        {
            
            
            
            
            //old version
            // if (_move.sqrMagnitude < 0.01) //  || _isAttacking
            //     return;
            // float targetAngle = Mathf.Atan2(_move.x,_move.y) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
            //     turnSmoothTime);
            // transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // _controller.Move(moveDirection.normalized * (_currentSpeed * Time.deltaTime));
        }
        #region Gameplay
        private void OnEnable()
        {
            OnDead += HandleDeath;
            OnHurt += HandleHurt;
            OnManaHealed += HandleManaHealed;
            OnHealthHealed += HandleHealthHealed;
        }

        private void OnDisable()
        {
            // OnDead -= HandleDeath;
            OnHurt -= HandleHurt;
            OnManaHealed -= HandleManaHealed;
            OnHealthHealed -= HandleHealthHealed;
        }
        public void OnResumeGame()
        {
            playerInput.enabled = true;
            playerInput.SwitchCurrentActionMap("gameplay");
        }
        
        public void OnPauseGame()
        {
            playerInput.enabled = true;
            playerInput.SwitchCurrentActionMap("Menu");
        }
        // public void OnMove(InputAction.CallbackContext context)
        // {
        //     _move = context.ReadValue<Vector2>();
        // }

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
            _cineMachine.m_YAxis.Value += value.y * Time.deltaTime * _cineMachine.m_YAxis.m_MaxSpeed;
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            GameManager.Instance.PauseGame();
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
            if (!GameManager.InstanceExists || !LevelManager.InstanceExists) return;
            
            if (LevelManager.Instance.sceneIndex == Enums.Scenes.Lobby)
            {
                if (GameManager.Instance.generalData.HasTeleportData)
                {
                    playerInput.enabled = false;
                    IsIntangible = true;
                    foreach (var particle in _particleSystemsTeleport)
                    {
                        particle.Play();
                    }
                    GameManager.Instance.HandleTeleport(null);
                }
            }
            else
            {
                var teleportItems = InventoryManager.Instance.inventory.GetItemsByType(ItemType.Teleport);
                if (teleportItems.Count > 0)
                {
                    playerInput.enabled = false;
                    IsIntangible = true;
                    foreach (var particle in _particleSystemsTeleport)
                    {
                        particle.Play();
                    }
                    var teleportItem = teleportItems[0].item;
                    InventoryManager.Instance.inventory.RemoveItem(teleportItem);
                    GameManager.Instance.HandleTeleport(new TeleportData(transform.position, LevelManager.Instance.sceneIndex));
                }
            }
        }

        public void OnVinesSkill(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            if (CooldownValue >= 0.99f && UseMana())
            {
                Instantiate(vinesSkillPrefab, transform.position, transform.rotation);
                StartCoroutine(VineSkillCooldown());
            }
        }

        IEnumerator VineSkillCooldown()
        {
            var time = delayTimeVineSkill;
            while (time > 0)
            {
                time -= Time.fixedDeltaTime;
                CooldownValue = (delayTimeVineSkill - time)/delayTimeVineSkill;
                yield return new WaitForFixedUpdate();
            }
            CooldownValue = 1;
            yield return null;
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
            if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
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
            playerInput.SwitchCurrentActionMap("Death");
        }
        private void HandleHurt(int damageTaken)
        {
        }
        private void HandleHealthHealed(int oldValue,int newValue)
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
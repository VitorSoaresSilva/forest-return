using System;
using System.Collections;
using ForestReturn.Scripts.Cameras;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class PlayerManager : BaseCharacter
    {
        private InputHandler _inputHandler;
        private AnimatorHandler _animatorHandler;
        private PlayerLocomotion _playerLocomotion;
        [HideInInspector] public PlayerInput playerInput;
        private Animator _animator;
        private CameraHandler _cameraHandler;
        private InventoryObject _inventoryObjectRef;
        [Header("Player config")]
        private float delayTimeVineSkill = 3;
        [Header("Player Flags")]
        public bool isInteracting;
        public bool isInAir;
        public bool isGrounded;
        [Header("Damage")] 
        [SerializeField] private GameObject swordHitBox;
        [SerializeField] private PlayerAttack[] attacks;
        [Header("Skills")]
        [SerializeField] private GameObject vinesSkillPrefab;
        public delegate void OnVineSkillCoolDownChangedEvent(float value);
        public event OnVineSkillCoolDownChangedEvent OnVineSkillCoolDownChanged;
        private float _cooldownVinesSkillValue;
        [SerializeField] private GameObject swordEffect;
        public ParticleSystem[] particleSystemsTeleport;
        public ParticleSystem[] particleSystemsHealth;

        [Header("Masks")] 
        [SerializeField] private TriggerObject lifeMaskTrigger;
        [SerializeField] private TriggerObject manaMaskTrigger;

        [SerializeField] private GameObject lifeMask;
        [SerializeField] private GameObject manaMask;
        
        [Header("Capsule")] 
        [SerializeField] private GameObject capsule;
        [SerializeField] private GameObject flower;
        
        [SerializeField] private TriggerObject capsuleTrigger;
        [SerializeField] private TriggerObject flowerTrigger;
        // public string vinesAttackAnimationName = "Vines";

        private float CooldownValue
        {
            get => _cooldownVinesSkillValue;
            set
            {
                _cooldownVinesSkillValue = value;
                OnVineSkillCoolDownChanged?.Invoke(value);
            }
        }
        protected override void Awake()
        {
            base.Awake();
            playerInput = GetComponent<PlayerInput>();
            _inputHandler = GetComponent<InputHandler>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
            _animator = GetComponentInChildren<Animator>();
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void Init()
        {
            _cameraHandler = CameraHandler.Instance;
            _playerLocomotion.Init();
            
            OnHurt += HandleHurt;
            
            if (InventoryManager.InstanceExists)
            {
                _inventoryObjectRef = InventoryManager.Instance.inventory;
            }
            if (LevelManager.InstanceExists)
            {
                transform.position = LevelManager.Instance.pointToSpawn;
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

            }
            InitSkill();
            UpdateMask();
            UpdateCapsule();
            UpdateAttacks();
        }

        private void HandleHurt(int damage)
        {
            _animatorHandler.PlayerTargetAnimation("Hurt",true);
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = _animator.GetBool("isInteracting");
            _inputHandler.TickInput(delta);
            if (GameManager.InstanceExists && !GameManager.Instance.IsPaused && !IsDead)
            {
                _playerLocomotion.HandleMovement(delta);
                _playerLocomotion.HandleRollingAndSprinting(delta);
                _playerLocomotion.HandleFalling(delta, _playerLocomotion.moveDirection);
            }
        }

        private void LateUpdate()
        {
            _inputHandler.rollFlag = false;
            if (isInAir)
            {
                _playerLocomotion.inAirTimer += Time.deltaTime;
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
            if (InventoryManager.InstanceExists)
            {
                return Damage + InventoryManager.Instance.equippedItems.swordInventorySlot.level * 2;
            }
            else
            {
                return Damage;
            }
        }
        private void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnResumeGame -= OnResumeGame;
                GameManager.Instance.OnPauseGame -= OnPauseGame;
            }
        }
        private void OnEnable()
        {
            OnDead += HandleDeath;
            // OnHurt += HandleHurt;
            // OnManaHealed += HandleManaHealed;
            // OnHealthHealed += HandleHealthHealed;
        }

        private void OnDisable()
        {
            OnDead -= HandleDeath;
            // OnHurt -= HandleHurt;
            // OnManaHealed -= HandleManaHealed;
            // OnHealthHealed -= HandleHealthHealed;
        }
        public void OnResumeGame()
        {
            // playerInput.enabled = true;
            // playerInput.SwitchCurrentActionMap("gameplay");
            _inputHandler.SwitchActionMap("gameplay");
        }
        
        public void OnPauseGame()
        {
            // playerInput.enabled = true;
            _inputHandler.SwitchActionMap("UI");
            // playerInput.SwitchCurrentActionMap("UI");
        }
        
        public void OnVinesSkill()
        {
            if (isInteracting) return;
            if (CooldownValue >= 0.99f && UseMana())
            {
                // _animatorHandler.PlayerTargetAnimation(vinesAttackAnimationName, true);
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
        private void HandleDeath()
        {
            _animatorHandler.PlayerTargetAnimation("Death",true);
            _inputHandler.SwitchActionMap("UI");
        }

        public void HandleManaPotion()
        {
            if (!(CurrentMana < MaxMana)) return;
            var potions = _inventoryObjectRef.GetPotionByType(PotionType.Mana);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                InventoryManager.Instance.inventory.RemoveItem(potion);
                ManaHeal(potion.value);
            }
        }
        public void HandleResumeGame()
        {
            if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }
        public void HandleTeleport()
        {
            if (!GameManager.InstanceExists || !LevelManager.InstanceExists) return;
            if (LevelManager.Instance.sceneIndex == Enums.Scenes.Lobby)
            {
                if (GameManager.Instance.generalData.HasTeleportData)
                {
                    //playerInput.enabled = false;
                    IsIntangible = true;
                    foreach (var particle in particleSystemsTeleport)
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
                    //playerInput.enabled = false;
                    IsIntangible = true;
                    foreach (var particle in particleSystemsTeleport)
                    {
                        particle.Play();
                    }
                    var teleportItem = teleportItems[0].item;
                    InventoryManager.Instance.inventory.RemoveItem(teleportItem);
                    GameManager.Instance.HandleTeleport(new TeleportData(transform.position, LevelManager.Instance.sceneIndex));
                }
            }
        }
        
        public void HandleLifePotion()
        {
            if (!(CurrentHealth < MaxHealth)) return;
            var potions = _inventoryObjectRef.GetPotionByType(PotionType.Life);
            if (potions.Count > 0)
            {
                var potion = (PotionObject)potions[0].item;
                InventoryManager.Instance.inventory.RemoveItem(potion);
                HealthHeal(potion.value); 
                foreach (var particle in particleSystemsHealth)
                {
                    particle.Play();
                }
            }
        }
        public void HandleEndComboAttack() // Disabled for a while
        {
            // _isAttacking = false;
            swordEffect.SetActive(false);
            swordHitBox.SetActive(false);
        }

        public void HandleResume()
        {
            if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }
        public void EnableHitBox()
        {
            swordHitBox.SetActive(true);
            swordEffect.SetActive(true);
        }

        public void DisableHitBox()
        {
            swordHitBox.SetActive(false);
            swordEffect.SetActive(false);
        }

        public void SetRollingIntangibility(bool state)
        {
            IsIntangible = state;
        }

        public void UpdateMask()
        {
            lifeMask.SetActive(false);
            manaMask.SetActive(false);
            if (InventoryManager.Instance.triggerInventory.Contains(lifeMaskTrigger))
            {
                lifeMask.SetActive(true);
                MaxHealth = 15;
                MaxMana = baseAttributes.mana;
            }else if (InventoryManager.Instance.triggerInventory.Contains(manaMaskTrigger))
            {
                manaMask.SetActive(true);
                MaxMana = 6;
                MaxHealth = baseAttributes.health;
            }
        }

        public void UpdateCapsule()
        {
            capsule.SetActive(false);
            flower.SetActive(false);
            if (InventoryManager.Instance.triggerInventory.Contains(capsuleTrigger))
            {
                capsule.SetActive(true);
                if (InventoryManager.Instance.triggerInventory.Contains(flowerTrigger))
                {
                    flower.SetActive(true);
                }
            }
        }
        
    }
}
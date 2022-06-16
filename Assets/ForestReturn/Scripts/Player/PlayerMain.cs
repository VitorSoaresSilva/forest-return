using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Character;
using Damage;
using Interactable;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Weapons;
// using Weapons;
using Attribute = Attributes.Attribute;

namespace Player
{
    [RequireComponent(typeof(WeaponHolder))]
    public class PlayerMain : BaseCharacter
    {
        private Rigidbody _rb;
        private Animator _animator;
        private PlayerInputAction _playerInputAction;
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isDashing;
        [SerializeField] [NotNull] private Camera _mainCamera;
        [SerializeField] private float camRayLength;
        private Quaternion _lastMouseRotation;

        // private PlayerAnimationManager _animationManager;
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
        private static readonly int Walking = Animator.StringToHash("isWalking");
        private static readonly int Dash = Animator.StringToHash("Dash");
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        private static readonly int Dead = Animator.StringToHash("Dead");
        
        [Header("Movement")]
        [SerializeField] private LayerMask floorMask;
        [SerializeField] private float dashSpeed = 8;
        [SerializeField] private float runSpeed = 6;
        [SerializeField] private float rotationRatio;
        private float _speed;
        private Matrix4x4 _matrix4X4;
        private Vector3 skewed;
        


        [Header("Som")] 
        public GameObject soundTrigger;

        private static readonly int AttackPunch = Animator.StringToHash("AttackPunch");
        [Header("Damage")] 
        [SerializeField] private GameObject damageHitBox;

        
        [Header("Interact")]
        [SerializeField] private Vector3 offsetInteract;
        [SerializeField] private float sphereInteractionRadius;
        private RaycastHit[] _raycastHits = new RaycastHit[10];

        // [SerializeField] private Weapon initialWeapon;
        // [Header("Weapon")]
        public WeaponHolder _weaponHolder { get; private set; }
        // private Weapon _weapon;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponentInChildren<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.gameplay.Enable();
            _matrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
            _speed = runSpeed;
            _weaponHolder = GetComponent<WeaponHolder>();
            if (_mainCamera == null && Camera.main != null)
            {
                _mainCamera = Camera.main;
            }
        }

        private void OnEnable()
        {
            _playerInputAction.gameplay.Attack.performed += HandleAttack;
            _playerInputAction.gameplay.Interact.performed += HandleInteract;
            _playerInputAction.gameplay.dash.performed += HandleDash;
            _playerInputAction.gameplay.inventory.performed += HandleInventory;
            OnHurt += HandlePlayerHurt;
            OnDead += HandlePlayerDead;
        }

        private void HandleInventory(InputAction.CallbackContext obj)
        {
            if (UiManager.instance != null)
            {
                // _playerInputAction.gameplay.Disable();
                UiManager.instance.ShowWeaponsInventory(_weaponHolder.GetWeapons());
            }
        }

        private void OnDisable()
        {
            _playerInputAction.gameplay.Attack.performed -= HandleAttack;
            _playerInputAction.gameplay.Interact.performed -= HandleInteract;
            _playerInputAction.gameplay.dash.performed -= HandleDash;
            _playerInputAction.gameplay.inventory.performed -= HandleInventory;
            OnHurt -= HandlePlayerHurt;
            OnDead -= HandlePlayerDead;
        }

        private void HandlePlayerDead()
        {
            // habilitar outro input
            _playerInputAction.gameplay.Disable();
            _animator.SetTrigger(Dead);
            skewed = Vector3.zero;
            _speed = 0;
            var hitBoxes = GetComponentsInChildren<HitBox>(true);
            foreach (var hitBox in hitBoxes)
            {
                hitBox.enabled = false;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void HandlePlayerHurt(Vector3 knockBackForce)
        {
            HandleEndAttack();
            _animator.SetTrigger(Hurt);
            _rigidbody.AddForce(knockBackForce, ForceMode.VelocityChange);
            if (UiManager.instance != null)
            {
                UiManager.instance.PlayerHurt();
            }
        }

        private void Start()
        {
            foreach (var attribute in attributes)
            {
                attribute.ChangedMaxValue += HandleAttributeMaxValueChanged;
                attribute.ChangedCurrentValue += HandleAttributeCurrentValueChanged;
                HandleAttributeMaxValueChanged(attribute);
                HandleAttributeCurrentValueChanged(attribute);
            }
        }

        #region  HandleChangeValues
        private void HandleAttributeMaxValueChanged(Attribute attribute)
        {
            if (UiManager.instance != null)
            {
                UiManager.instance.UpdateMaxValueAttribute(attributes[(int)attribute.Type]);
            }
        }
        private void HandleAttributeCurrentValueChanged(Attribute attribute)
        {
            if (UiManager.instance != null)
            {
                UiManager.instance.UpdateCurrentValueAttribute(attributes[(int)attribute.Type]);
            }
        }
        #endregion
        private void OnDestroy()
        {
            foreach (var attribute in attributes)
            {
                attribute.ChangedMaxValue -= HandleAttributeMaxValueChanged;
                attribute.ChangedCurrentValue -= HandleAttributeCurrentValueChanged;
            }
        }
        private void HandleDash(InputAction.CallbackContext obj)
        {
            if (isAttacking || isDashing || !_playerInputAction.gameplay.enabled) return;
            Vector2 playerInput = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            _playerInputAction.gameplay.Disable();
            skewed = _matrix4X4.MultiplyPoint3x4(new Vector3(playerInput.x,0,playerInput.y));
            if (skewed.magnitude < 0.01f)
            {
                skewed = transform.forward;
            }
            if (playerInput.magnitude > 0.01f)
            {
                var rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(skewed,Vector3.up), rotationRatio*10).normalized;
                _rb.MoveRotation(rot);
            }
            _animator.SetTrigger(Dash);
        }

        public void HandleAnimationDashEnd()
        {
            _speed = runSpeed;
            isDashing = false;
            isIntangible = false;
            _playerInputAction.gameplay.Enable();
        }

        public void HandleAnimationDashStart()
        {
            _speed = dashSpeed;
            isDashing = true;
            isIntangible = true;
        }

        private void Update()
        {
            if (!_playerInputAction.gameplay.enabled) return;
            var playerInput = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            skewed = _matrix4X4.MultiplyPoint3x4(new Vector3(playerInput.x,0,playerInput.y));
            _animator.SetBool(Walking,skewed.magnitude > 0.01f);
            _animator.SetFloat(VelocityX,Vector3.Dot(skewed,transform.forward));
            _animator.SetFloat(VelocityY,Vector3.Dot(skewed,transform.right));
        }

        private void FixedUpdate()
        {
            if (!isAttacking)
            {
                Move();
            }
            Turning();
        }

        private void Turning()
        {
            if (!_playerInputAction.gameplay.enabled) return;
            if (Mouse.current.position.ReadValueFromPreviousFrame() != Mouse.current.position.ReadValue())
            {
                Ray camRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(camRay, out var floorHit, camRayLength, floorMask))
                {
                    Vector3 playerToMouse = floorHit.point - transform.position;
                    playerToMouse.y = 0;
                    _lastMouseRotation = Quaternion.LookRotation(playerToMouse.normalized, Vector3.up);
                }
            }
            
            if (!(Quaternion.Angle(transform.rotation, _lastMouseRotation) > 0.01f)) return;
            var rot = Quaternion.Slerp(transform.rotation, _lastMouseRotation,rotationRatio).normalized;
            _rb.MoveRotation(rot);
        }

        

        private void Move()
        {
            if (skewed.magnitude < 0.01f) return;
            Vector3 vel = skewed * _speed;
            vel.y = _rb.velocity.y;
            _rb.velocity = vel;
        }

        private void HandleInteract(InputAction.CallbackContext obj)
        {
            int hits = Physics.SphereCastNonAlloc(
                transform.position + offsetInteract.x * transform.forward + offsetInteract.y * transform.up,
                sphereInteractionRadius, transform.forward, _raycastHits);
            var interactables = new List<IInteractable>();
            int closestIndex = 0;
            // Debug.Log(hits);
            for (int i = 0; i < hits; i++)
            {
                // Debug.Log("for" + hits);
                if (!_raycastHits[i].transform.TryGetComponent(out IInteractable interactable)) continue;
                interactables.Add(interactable);
                // interactable.
                if (_raycastHits[i].distance < _raycastHits[closestIndex].distance)
                {
                    closestIndex = interactables.Count - 1;
                }
            }

            if (interactables.Count > 0)
            {
                interactables[closestIndex].Interact();
            }
        }

        public void HandleStartAttack()
        {
            // setar o trigger
            damageHitBox.SetActive(true);
        }

        public void HandleEndAttack()
        {
            // desligar trigger
            damageHitBox.SetActive(false);
            isAttacking = false;
        }
        private void HandleAttack(InputAction.CallbackContext obj)
        {
            if (isAttacking || isAttacking) return;
            isAttacking = true;
            _animator.SetTrigger(AttackPunch);
        }

        public void HandleTeleportActivated(Vector3 position)
        {
            _playerInputAction.gameplay.Disable();
            _animator.SetTrigger("TelePortPart");
        }

        public void HandleAnimationTeleportPartOneEnd()
        {
            
        }
        public void HandleStepSound()
        {
            if (soundTrigger == null) return;
            soundTrigger.SetActive(true);
            Invoke(nameof(DisableFootStepSound),0.5f);
        }
        public void DisableFootStepSound()
        {
            soundTrigger.SetActive(false);
        }

        // public void EquipNewWeapon(WeaponsScriptableObject weaponsScriptableObject)
        // {
        //     // if (Weapon == null)
        //     // {
        //     //     Weapon = new Weapon(this, weaponsScriptableObject);
        //     //     // Weapon = initialArtifactsToWeapon == null ? 
        //     //     //     new Weapon(this, initialWeaponData) : 
        //     //     //     new Weapon(this, initialWeaponData, initialArtifactsToWeapon);
        //     // }
        // }
    }
}
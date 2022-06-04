using System;
using System.Diagnostics.CodeAnalysis;
using Character;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Attribute = Attributes.Attribute;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimationManager))]
    public class PlayerMain : BaseCharacter
    {
        private Rigidbody _rb;
        private Animator _animator;
        private PlayerInputAction _playerInputAction;
        private Vector3 _velocity;
        private Vector3 _desiredVelocity;
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isDashing;
        [SerializeField] [NotNull] private Camera _mainCamera;
        
        [SerializeField, Range(0f, 100f)] private float maxSpeed = 5f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 6f;
        
        [SerializeField] private Transform playerInputSpace;
        [SerializeField] private float camRayLength;
        [SerializeField] private LayerMask floorMask;
        private Quaternion _lastMouseRotation;
        [SerializeField] private float rotationRatio;

        private PlayerAnimationManager _animationManager;
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
        private static readonly int Walking = Animator.StringToHash("isWalking");
        private static readonly int Dash = Animator.StringToHash("Dash");
        [SerializeField] private float maxSpeedDash = 10;


        [Header("Som")] public GameObject soundTrigger;
        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponentInChildren<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.gameplay.Enable();
            _playerInputAction.gameplay.Attack.performed += HandleAttack;
            _playerInputAction.gameplay.Interact.performed += HandleInteract;
            _playerInputAction.gameplay.dash.performed += HandleDash;
            if (_mainCamera == null && Camera.main != null)
            {
                _mainCamera = Camera.main;
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
            isDashing = true;
            
            Vector2 playerInput = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            Vector3 dir = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y);
            if (dir.magnitude == 0)
            {
                dir = transform.forward;
            }
            _playerInputAction.gameplay.Disable();
            _desiredVelocity = dir * maxSpeedDash;
            _animator.SetTrigger(Dash);
        }

        public void HandleAnimationDashEnd()
        {
            isDashing = false;
            _playerInputAction.gameplay.Enable();
        }

        private void Update()
        {
            var playerInput = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            if (_playerInputAction.gameplay.enabled)
            {
                // Debug.Log(playerInput + " " + playerInput.normalized);
                playerInput = playerInput.normalized;
               _desiredVelocity = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeed;
               // _desiredVelocity = new Vector3(playerInput.x,0,playerInput.y) * maxSpeed;
                _animator.SetBool(Walking,_velocity.magnitude > 0.01f);
                _animator.SetFloat(VelocityX,Vector3.Dot(_desiredVelocity.normalized,transform.forward));
                _animator.SetFloat(VelocityY,Vector3.Dot(_desiredVelocity.normalized,transform.right));
            }
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
            _velocity = _rb.velocity;
            var maxSpeedChange = maxAcceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
            _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, maxSpeedChange);
            _rb.velocity = _velocity;
        }

        private void HandleInteract(InputAction.CallbackContext obj)
        {
            // throw new NotImplementedException();
        }

        private void HandleAttack(InputAction.CallbackContext obj)
        {
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
            soundTrigger.SetActive(true);
            Invoke(nameof(DisableFootStepSound),0.5f);
        }
        public void DisableFootStepSound()
        {
            soundTrigger.SetActive(false);
        }
    }
}
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

        protected override void Awake()
        {
            base.Awake();
            foreach (var attribute in attributes)
            {
                attribute.ChangedMaxValue += HandleAttributeMaxValueChanged;
                attribute.ChangedCurrentValue += HandleAttributeCurrentValueChanged;
                HandleAttributeMaxValueChanged(attribute);
                HandleAttributeCurrentValueChanged(attribute);
            }
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
        #region  HandleChangeValues
        private void HandleAttributeMaxValueChanged(Attribute attribute)
        {
            if (UiManager.instance != null)
            {
                UiManager.instance.UpdateMaxValueAttribute(attribute.Type, attributes[(int)attribute.Type].MaxValue);
                Debug.Log("HandleAttributeMaxValueChanged");
            }
        }
        private void HandleAttributeCurrentValueChanged(Attribute attribute)
        {
            if (UiManager.instance != null)
            {
                UiManager.instance.UpdateCurrentValueAttribute(attribute.Type, attributes[(int)attribute.Type].CurrentValue);
                Debug.Log("HandleAttributeCurrentValueChanged");
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
            
            var playerInput = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            // var rot = Quaternion.Slerp(_rb.transform.rotation, Quaternion.LookRotation(playerInput.normalized, Vector3.up),10).normalized;
            // _rb.MoveRotation(rot);
            if (playerInput.magnitude == 0)
            {
                playerInput = _rb.transform.forward;
            }
            _playerInputAction.gameplay.Disable();
            _desiredVelocity = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeedDash;
            _animator.SetTrigger(Dash);
        }

        public void HandleAnimationEnd()
        {
            isDashing = false;
            _playerInputAction.gameplay.Enable();
        }

        private void Update()
        {
            var playerInput = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            playerInput = Vector2.ClampMagnitude(playerInput, 1);
            if (_playerInputAction.gameplay.enabled)
            {
               _desiredVelocity = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeed;
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
            // throw new NotImplementedException();
        }

        public void HandleTeleportActivated(Vector3 position)
        {
            
        }
    }
}
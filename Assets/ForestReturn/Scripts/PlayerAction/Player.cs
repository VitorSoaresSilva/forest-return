using System;
using System.Collections.Generic;
using Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ForestReturn.Scripts.PlayerAction
{
    public class Player : BaseCharacter
    {
        [Header("Movement")]
        private Vector2 _move; 
        private Vector2 _look;
        private Vector3 _rotation;
        private float _speed; 
        [SerializeField] private float rotationRatio;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float turnSmoothTime = 0.1f;
        public Transform cam;
        
        private CharacterController _controller;
        private Animator _animator;
        [Header("Interact")]
        [SerializeField] private Vector3 offsetInteract;
        [SerializeField] private float sphereInteractionRadius;
        private readonly RaycastHit[] _raycastHits = new RaycastHit[3];
        private bool _isAttacking;
        private bool _isDashing;
        private float turnSmoothVelocity;
        
        // Animations
        private static readonly int AttackPunch = Animator.StringToHash("Attack");
        private static readonly int Walking = Animator.StringToHash("isMoving");
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            _speed = moveSpeed;
        }

        private void Update()
        {
            Move();
            _animator.SetBool(Walking,_move.sqrMagnitude > 0.01f);
        }
        
        private void Move()
        {
            if (_move.sqrMagnitude < 0.01)
                return;
            float targetAngle = Mathf.Atan2(_move.x,_move.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDirection.normalized * (_speed * Time.deltaTime));
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }
        
        public void OnLook(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (_isAttacking) return;
            _isAttacking = true;
            
        }
        
        public void OnInteract(InputAction.CallbackContext context)
        {
            // TODO: testar
            int hits = Physics.SphereCastNonAlloc(
                transform.position + offsetInteract.x * transform.forward + offsetInteract.y * transform.up,
                sphereInteractionRadius, transform.forward, _raycastHits);
            var interactableList = new List<IInteractable>();
            int closestIndex = 0;
            for (int i = 0; i < hits; i++)
            {
                if (!_raycastHits[i].transform.TryGetComponent(out IInteractable interactable)) continue;
                interactableList.Add(interactable);
                if (_raycastHits[i].distance < _raycastHits[closestIndex].distance)
                {
                    closestIndex = interactableList.Count - 1;
                }
            }
            if (interactableList.Count > 0)
            {
                interactableList[closestIndex].Interact();
            }
        }
    }
}
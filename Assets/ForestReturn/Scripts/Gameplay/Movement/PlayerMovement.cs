using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    [SerializeField] private Transform playerInputSpace = default;
    [SerializeField] private Quaternion _lastMouseRotation;
    [SerializeField] private float rotationRatio = 0.1f;
    [SerializeField] [NotNull] private Camera mainCamera;
    [SerializeField] private float camRayLength = 100f;
    
    [SerializeField] private LayerMask floorMask;
    private Rigidbody _rb;
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;
    private bool isAttacking;
    [SerializeField] private Animator _animator;
    private PlayerInput _playerInput;

    private const string IsWalking = "isWalking";
    private const string AnimatorVelocity = "Direction";
    private const string AnimatorVelocityX = "VelocityX";
    private const string AnimatorVelocityY = "VelocityY";
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (mainCamera == null && Camera.main != null)
        {
            mainCamera = Camera.main;
        }
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Update()
    {
        
        // Debug.Log(Quaternion.Angle(transform.rotation,_lastMouseRotation));
        var playerInput = _playerInput.gameplay.move.ReadValue<Vector2>();
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        _desiredVelocity = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeed;
        

        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     Attack();
        // }

        // Move(playerInput);
        _animator.SetBool(IsWalking,_velocity.magnitude > 0.01f);
        // _animator.SetFloat(AnimatorVelocity,Vector3.Dot(_desiredVelocity.normalized,playerInputSpace.forward));
        _animator.SetFloat(AnimatorVelocityX,Vector3.Dot(_desiredVelocity.normalized,transform.forward));
        _animator.SetFloat(AnimatorVelocityY,Vector3.Dot(_desiredVelocity.normalized,transform.right));
        Turning();
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
        {
            return;
        }
        var scaledMoveSpeed = maxSpeed * Time.deltaTime;
        // var move = Quate
        // var move = Quate
        // Debug.Log("opa " + scaledMoveSpeed);
        var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Attack()
    {
        Debug.Log("Trying to attack");
        if (!isAttacking)
        {
            isAttacking = true;
            _animator.SetTrigger("Attack");
        }
    }

    public void SetEndAnimationAttack()
    {
        isAttacking = false;
    }
    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
        _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, maxSpeedChange);
        _rb.velocity = _velocity;
    }
    void Turning()
    {
        // se o mouse moveu eu mudo a posição
        // se nao eu tento chegar no target
        
        
        if (Mouse.current.position.ReadValueFromPreviousFrame() != Mouse.current.position.ReadValue())
        {
            Ray camRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(camRay, out var floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                _lastMouseRotation = Quaternion.LookRotation(playerToMouse.normalized, Vector3.up);
                // _rb.MoveRotation(_lastMouseRotation);
                // _lastMousePosition = playerToMouse;
            }
        }
        //
        if (Quaternion.Angle(transform.rotation,_lastMouseRotation) > 0.01f)
        {
            var rot = Quaternion.Slerp(transform.rotation, _lastMouseRotation,rotationRatio).normalized;
            _rb.MoveRotation(rot);
        }
    }
    
}

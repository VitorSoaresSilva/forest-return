using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    [SerializeField] private Transform playerInputSpace = default;
    
    [SerializeField] [NotNull] private Camera mainCamera;
    [SerializeField] private float camRayLength = 100f;
    
    [SerializeField] private LayerMask floorMask;
    private Rigidbody _rb;
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;
    private bool isAttacking;
    [SerializeField] private Animator _animator;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (mainCamera == null && Camera.main != null)
        {
            mainCamera = Camera.main;
        }
    }
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        if (playerInputSpace)
        {
            _desiredVelocity = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeed;
        }else
        {
            _desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * maxSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        Turning();
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
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out var floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _rb.MoveRotation(newRotation);
        }
    }
    
}

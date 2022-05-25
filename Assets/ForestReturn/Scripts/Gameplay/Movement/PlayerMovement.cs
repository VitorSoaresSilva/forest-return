using System.Collections.Generic;
using Interactable;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    [SerializeField] private Transform playerInputSpace = default;
    [SerializeField] private Quaternion lastMouseRotation;
    [SerializeField] private float rotationRatio = 0.1f;
    [SerializeField] [NotNull] private Camera _mainCamera;
    [SerializeField] private float camRayLength = 100f;
    
    [SerializeField] private LayerMask floorMask;
    private Rigidbody _rb;
    private Animator _animator;
    private PlayerInputAction _playerInput;
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;
    private bool _isAttacking;
    private static readonly int VelocityX = Animator.StringToHash("VelocityX");
    private static readonly int VelocityY = Animator.StringToHash("VelocityY");
    private static readonly int Walking = Animator.StringToHash("isWalking");
    private static readonly int TeleportFirstPart = Animator.StringToHash("teleportFirstPart");
    private static readonly int TeleportSecondPart = Animator.StringToHash("teleportSecondPart");
    [SerializeField] private float sphereInteractionRadius;

    [SerializeField] private Vector3 offsetInteract;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private Vector3 teleportTargetPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator =  GetComponentInChildren<Animator>();
        if (_mainCamera == null && Camera.main != null)
        {
            _mainCamera = Camera.main;
        }
        _playerInput = new PlayerInputAction();
        _playerInput.gameplay.Attack.performed += HandleAttack;
        _playerInput.gameplay.Interact.performed += HandleInteract;
    }

    private void HandleInteract(InputAction.CallbackContext context)
    {
        int hits = Physics.SphereCastNonAlloc(
            transform.position + offsetInteract.x * transform.forward + offsetInteract.y * transform.up,
            sphereInteractionRadius, transform.forward, _raycastHits);
        var interactables = new List<IInteractable>();
        int closestIndex = 0;
        Debug.Log(hits);
        for (int i = 0; i < hits; i++)
        {
        Debug.Log("for" + hits);
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

    #region Teleport
    public void HandleTeleportActivated(Vector3 targetPosition)
    {
        /*
         * stop input
         * start animation
         */
        // _playerInput.Disable();
        _animator.SetTrigger(TeleportFirstPart);
        // teleportTargetPosition = targetPosition;
    }   

    public void HandleEndFirstPartTeleport()
    {
        // teleportar o player
        transform.position = teleportTargetPosition;
        _animator.SetTrigger(TeleportSecondPart);
    }
    public void HandleEndTeleport()
    {
        _playerInput.gameplay.Enable();
    }
    #endregion
    

    private void HandleAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Trying to attack");
        if (!_isAttacking && context.performed)
        {
            _isAttacking = true;
            _animator.SetTrigger("Attack");
        }
    }

    private void OnEnable()
    {
        _playerInput.gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Update()
    {
        var playerInput = _playerInput.gameplay.move.ReadValue<Vector2>();
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        _desiredVelocity = playerInputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeed;
        _animator.SetBool(Walking,_velocity.magnitude > 0.01f);
        _animator.SetFloat(VelocityX,Vector3.Dot(_desiredVelocity.normalized,transform.forward));
        _animator.SetFloat(VelocityY,Vector3.Dot(_desiredVelocity.normalized,transform.right));
    }

    // private void Move(Vector2 direction)
    // {
    //     if (direction.sqrMagnitude < 0.01) { return; }
    //     var scaledMoveSpeed = maxSpeed * Time.deltaTime;
    //     var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
    //     transform.position += move * scaledMoveSpeed;
    // }
    public void SetEndAnimationAttack()
    {
        _isAttacking = false;
    }
    private void FixedUpdate()
    {
        if (!_isAttacking)
        {
            Move();
        }
        Turning();
    }

    private void Move()
    {
        _velocity = _rb.velocity;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
        _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, maxSpeedChange);
        _rb.velocity = _velocity;
    }

    void Turning()
    {
        if (Mouse.current.position.ReadValueFromPreviousFrame() != Mouse.current.position.ReadValue())
        {
            Ray camRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(camRay, out var floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                lastMouseRotation = Quaternion.LookRotation(playerToMouse.normalized, Vector3.up);
            }
        }
        if (!(Quaternion.Angle(transform.rotation, lastMouseRotation) > 0.01f)) return;
        var rot = Quaternion.Slerp(transform.rotation, lastMouseRotation,rotationRatio).normalized;
        _rb.MoveRotation(rot);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + offsetInteract.x * transform.forward + offsetInteract.y * transform.up, sphereInteractionRadius);
    }
    
}

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using _Developers.Vitor.Scripts.Character;
using _Developers.Vitor.Scripts.Damage;
using _Developers.Vitor.Scripts.Interactable;
using _Developers.Vitor.Scripts.Weapons;
using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
// using Weapons;
using Attribute = _Developers.Vitor.Scripts.Attributes.Attribute;

namespace _Developers.Vitor.Scripts.Player
{
    // [RequireComponent(typeof(WeaponHolder))]
    public class PlayerMain : BaseCharacter
    {
        private Rigidbody _rb;
        private Animator _animator;
        private PlayerInputAction _playerInputAction;
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isDashing;
        [SerializeField] [NotNull] private UnityEngine.Camera _mainCamera;
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
        // private Matrix4x4 _matrix4X4;
        // private Vector3 skewed;
        private Vector2 m_Rotation;
        


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
            // _matrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0,0,0));
            _speed = runSpeed;
            _weaponHolder = GetComponent<WeaponHolder>();
            if (_mainCamera == null && UnityEngine.Camera.main != null)
            {
                _mainCamera = UnityEngine.Camera.main;
            }
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _playerInputAction.gameplay.Attack.performed += HandleAttack;
            _playerInputAction.gameplay.Interact.performed += HandleInteract;
            _playerInputAction.gameplay.dash.performed += HandleDash;
            _playerInputAction.gameplay.inventory.performed += HandleInventory;
            _playerInputAction.gameplay.menu.performed += HandleMenu;
            
            
            OnHurt += HandlePlayerHurt;
            OnDead += HandlePlayerDead;
        }

        private void HandleMenu(InputAction.CallbackContext obj)
        {
            // LevelManager.instance.UiMenuOpen();
        }

        private void HandleInventory(InputAction.CallbackContext obj)
        {
            if (UiManager.instance != null)
            {
                // UiManager.instance.ShowArtifactInventory();
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
            // skewed = Vector3.zero;
            _speed = 0;
            var hitBoxes = GetComponentsInChildren<HitBoxOld>(true);
            foreach (var hitBox in hitBoxes)
            {
                hitBox.enabled = false;
            }
            // UiManager.instance.ShowDeathPanel();
            // GameManager.instance.LoadScene(Enums.Scenes.Lobby);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                // UiManager.instance.UpdateMaxValueAttribute(attributes[(int)attribute.Type]);
            }
        }
        private void HandleAttributeCurrentValueChanged(Attribute attribute)
        {
            if (UiManager.instance != null)
            {
                // UiManager.instance.UpdateCurrentValueAttribute(attributes[(int)attribute.Type]);
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
            // skewed = _matrix4X4.MultiplyPoint3x4(new Vector3(playerInput.x,0,playerInput.y));
            if (playerInput.magnitude < 0.01f)
            {
                playerInput = transform.forward;
            }
            if (playerInput.magnitude > 0.01f)
            {
                var rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerInput,Vector3.up), rotationRatio*10).normalized;
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
            var input = _playerInputAction.gameplay.move.ReadValue<Vector2>();
            _animator.SetBool(Walking,input.magnitude > 0.01f);
            var test = new Vector3(input.y, 0, input.x);
            var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(input.x, 0, input.y);
            Debug.Log(test + " " + transform.right + " " + move);
            _animator.SetFloat(VelocityX,Vector3.Dot(move,transform.right));
            // _animator.SetFloat(VelocityY,Vector3.Dot(test,transform.forward));
            Turning2();
            Move(input);
        }

        private void FixedUpdate()
        {
            
        }

        private void Turning2()
        {
            // if (!_playerInputAction.gameplay.enabled) return;
            // var rotate = _playerInputAction.gameplay.look.ReadValue<Vector2>();
            //
            // if (rotate.sqrMagnitude < 0.01)
            //     return;
            // var scaledRotateSpeed = rotationRatio * Time.deltaTime;
            // m_Rotation.y += rotate.x * scaledRotateSpeed;
            // // m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
            // transform.localEulerAngles = m_Rotation;
        }

        private void Turning()
        {
            if (!_playerInputAction.gameplay.enabled) return;
            if (Mouse.current.position.ReadValueFromPreviousFrame() != Mouse.current.position.ReadValue())
            {
                Vector3 mouse = Mouse.current.position.ReadValue();
                mouse.z = 500f;
                Ray camRay = _mainCamera.ScreenPointToRay(mouse);
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

        

        private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01)
                return;
            var scaledMoveSpeed = _speed * Time.deltaTime;
            // For simplicity's sake, we just keep movement in a single plane here. Rotate
            // direction according to world Y rotation of player.
            var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
            transform.position += move * scaledMoveSpeed;
            
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
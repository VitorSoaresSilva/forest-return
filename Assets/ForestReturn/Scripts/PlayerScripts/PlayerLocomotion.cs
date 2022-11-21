using System;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private Transform _cameraObject;
        private InputHandler _inputHandler;
        public Vector3 moveDirection;
        private PlayerManager _playerManager;
        
        [HideInInspector] public Transform myTransform;
        [HideInInspector] public AnimatorHandler animatorHandler;
        
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Ground & Air Detection Stats")] 
        [SerializeField] private float groundDetectionRayStartPoint = 0.5f;
        [SerializeField] private float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField] private float groundDirectionRayDistance = 0.2f;
        private LayerMask _ignoreForGroundCheck;
        public float inAirTimer;
        
        
        [Header("Movement Stats")] 
        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float fallingSpeed = 45;

        public void Init()
        {
            rigidbody = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            _playerManager = GetComponent<PlayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            _cameraObject = LevelManager.Instance.CamerasHolder.mainCamera.transform;
            myTransform = transform;
            animatorHandler.Initialize();

            _playerManager.isGrounded = true;
            _ignoreForGroundCheck = ~(1 << 7 | 1 << 17);
        }



        #region Movement

        private Vector3 _normalVector;
        private Vector3 _targetPosition;

        private void HandlerRotation(float delta)
        {
            float moveOverride = _inputHandler.moveAmount;
            var targetDir = _cameraObject.forward * _inputHandler.vertical;
            targetDir += _cameraObject.right * _inputHandler.horizontal;
            
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            float rs = rotationSpeed;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }
        public void HandleMovement(float delta)
        {
            if (_inputHandler.rollFlag) return;
            if (_playerManager.isInteracting) return;
            
            moveDirection = _cameraObject.forward * _inputHandler.vertical;
            moveDirection += _cameraObject.right * _inputHandler.horizontal;
            moveDirection.y = 0;
            moveDirection.Normalize();
            
            float speed = movementSpeed;
            moveDirection *= speed;
            
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection,_normalVector);
            rigidbody.velocity = projectedVelocity;
            animatorHandler.UpdateAnimatorValue(_inputHandler.moveAmount, 0);

            if (animatorHandler.canRotate)
            {
                HandlerRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (_playerManager.isInteracting) return;

            if (_inputHandler.rollFlag)
            {
                moveDirection = _cameraObject.forward * _inputHandler.vertical;
                moveDirection += _cameraObject.right * _inputHandler.horizontal;
                if (_inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayerTargetAnimation("Rolling", true);
                    // animatorHandler.anim.applyRootMotion = true; //
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayerTargetAnimation("BackStep",true);
                }
            }
        }

        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            _playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;
            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            if (_playerManager.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;
            _targetPosition = myTransform.position;
            
            Debug.DrawRay(origin, - Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, _ignoreForGroundCheck))
            {
                _normalVector = hit.normal;
                Vector3 tp = hit.point;
                _playerManager.isGrounded = true;
                _targetPosition.y = tp.y;
                if (_playerManager.isInAir)
                {
                    if (inAirTimer > 0.5f)
                    {
                        // animatorHandler.PlayerTargetAnimation("Land", true);
                        inAirTimer = 0;
                    }
                    else
                    {
                        animatorHandler.PlayerTargetAnimation("Locomotion", false);
                        inAirTimer = 0;
                    }

                    _playerManager.isInAir = false;
                }
            }
            else
            {
                if (_playerManager.isGrounded)
                {
                    _playerManager.isGrounded = false;
                }

                if (_playerManager.isInAir == false)
                {
                    if (_playerManager.isInteracting == false)
                    {
                        // animatorHandler.PlayerTargetAnimation("Falling",true);
                    }

                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    _playerManager.isInAir = true;
                }
            }

            if (_playerManager.isGrounded)
            {
                if (_playerManager.isInteracting || _inputHandler.moveAmount > 0)
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, _targetPosition, Time.deltaTime);
                }
                else
                {
                    myTransform.position = _targetPosition;
                }
            }
            
        }

        #endregion
    }
}
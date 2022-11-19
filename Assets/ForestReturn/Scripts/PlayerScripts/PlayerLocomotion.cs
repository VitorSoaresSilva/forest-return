using System;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private Transform _cameraObject;
        private InputHandler _inputHandler;
        private Vector3 _moveDirection;
        
        [HideInInspector] public Transform myTransform;
        [HideInInspector] public AnimatorHandler animatorHandler;
        
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")] 
        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float rotationSpeed = 10;

        public void Init()
        {
            rigidbody = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            _cameraObject = LevelManager.Instance.CamerasHolder.mainCamera.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            
            _inputHandler.TickInput(delta);
            HandleMovement(delta);
            HandleRollingAndSprinting(delta);
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
        private void HandleMovement(float delta)
        {
            _moveDirection = _cameraObject.forward * _inputHandler.vertical;
            _moveDirection += _cameraObject.right * _inputHandler.horizontal;
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            
            float speed = movementSpeed;
            _moveDirection *= speed;
            
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection,_normalVector);
            rigidbody.velocity = projectedVelocity;
            animatorHandler.UpdateAnimatorValue(_inputHandler.moveAmount, 0);

            if (animatorHandler.canRotate)
            {
                HandlerRotation(delta);
            }
        }

        private void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.anim.GetBool("isInteracting")) return;

            if (_inputHandler.rollFlag)
            {
                _moveDirection = _cameraObject.forward * _inputHandler.vertical;
                _moveDirection += _cameraObject.right * _inputHandler.horizontal;
                if (_inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayerTargetAnimation("Rolling", true);
                    _moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(_moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayerTargetAnimation("BackStep",true);
                }
            }
        }

        #endregion
    }
}
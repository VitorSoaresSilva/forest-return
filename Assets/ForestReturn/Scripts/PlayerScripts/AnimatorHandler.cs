using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        private InputHandler _inputHandler;
        private PlayerLocomotion _playerLocomotion;
        private PlayerManager _playerManager;

        private int _vertical;
        private int _horizontal;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            _inputHandler = GetComponentInParent<InputHandler>();
            _playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            _playerManager = GetComponentInParent<PlayerManager>();
            _vertical = Animator.StringToHash("Vertical");
            _horizontal = Animator.StringToHash("Horizontal");

        }
        public void UpdateAnimatorValue(float verticalMovement, float horizontalMovement)
        {
            #region Vertical

            float v = 0;
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }else if (verticalMovement > 0.55f)
            {
                v = 1;
            }else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion
            
            anim.SetFloat(_vertical,v,0.1f,Time.deltaTime);
            anim.SetFloat(_horizontal,h,0.1f,Time.deltaTime);
        }

        public void PlayerTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting",isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
        
        public void CanRotate()
        {
            canRotate = true;
        }
        
        public void StopRotation()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (_playerManager.isInteracting == false) return;
            
            float delta = Time.deltaTime;
            _playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            _playerLocomotion.rigidbody.velocity = velocity;

        }
    }
}
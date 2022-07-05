using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [HideInInspector] public PlayerMain playerMainRef;
        [HideInInspector] public Rigidbody playerRBRef;
        [SerializeField] private float dragHigher = 4;

        private void Awake()
        {
            playerMainRef = GetComponentInParent<PlayerMain>();
            playerRBRef = GetComponentInParent<Rigidbody>();
        }

        public void SetStartAnimationAttack()
        {
            playerMainRef.HandleStartAttack();
        }
        public void SetEndAnimationAttack()
        {
            playerMainRef.HandleEndAttack();
        }

        public void SetEndAnimationDash()
        {
            playerMainRef.HandleAnimationDashEnd();
        }
        public void SetStartAnimationDash()
        {
            playerMainRef.HandleAnimationDashStart();
        }

        public void SetEndAnimationTeleportPartOne()
        {
            playerMainRef.HandleAnimationTeleportPartOneEnd();
        }
        public void SetTriggerStep(){
            playerMainRef.HandleStepSound();
        }

        public void SetDragHigher()
        {
            playerRBRef.drag = dragHigher;
        }

        public void SetDragLower()
        {
            playerRBRef.drag = 1;
            
        }
        
    }
}
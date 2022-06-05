using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [HideInInspector] public PlayerMain playerMainRef;

        private void Awake()
        {
            playerMainRef = GetComponentInParent<PlayerMain>();
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
        
    }
}
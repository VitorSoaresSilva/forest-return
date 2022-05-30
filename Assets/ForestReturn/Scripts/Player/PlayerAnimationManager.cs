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

        public void SetEndAnimationAttack()
        {
            playerMainRef.isAttacking = false;
        }

        public void SetEndAnimationDash()
        {
            playerMainRef.HandleAnimationEnd();
        }
    }
}
using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        // public Rigidbody playerRigidbody;

        private void Awake()
        {
            // playerRigidbody = GetComponentInParent<Rigidbody>();
            player = GetComponentInParent<Player>();
        }

        public void AttackEnd()
        {
            player.HandleEndAttack();
        }

        public void StartAnimationAttack()
        {
            player.HandleStartAttack();
        }

        public void StartRangedAttack()
        {
            player.HandleEndRangedAttack();
        }

        public void EndRangedAttack()
        {
            player.HandleStartRangedAttack();
        }
    }
}
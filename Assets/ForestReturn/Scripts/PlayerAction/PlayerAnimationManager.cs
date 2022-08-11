using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        public Rigidbody playerRigidbody;

        private void Awake()
        {
            playerRigidbody = GetComponentInParent<Rigidbody>();
            player = GetComponentInParent<Player>();
        }

        public void AttackEnd()
        {
            
        }
    }
}
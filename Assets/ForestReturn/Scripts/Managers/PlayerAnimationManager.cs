using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        // public Rigidbody playerRigidbody;

        private void Awake()
        {
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

        public void StartRangeSecondAttack()
        {
            player.HandleStartRangeSecondAttack();
        }

        public void EndRangeSecondAttack()
        {
            player.HandleEndRangeSecondAttack();
        }
    }
}
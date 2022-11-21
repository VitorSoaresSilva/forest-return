using ForestReturn.Scripts.PlayerScripts;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager player;
        // public Rigidbody playerRigidbody;

        private void Awake()
        {
            player = GetComponentInParent<PlayerManager>();
        }

        public void EnableHitBox()
        {
            player.EnableHitBox();
            
        }

        public void DisableHitBox()
        {
            player.DisableHitBox();
        }

        public void EndAttack()
        {
            player.HandleEndComboAttack();
        }

        // public void StartAcceptComboAttack()
        // {
        //     player.HandleStartRangeSecondAttack();
        // }
        // public void StopAcceptComboAttack()
        // {
        //     player.HandleEndRangeSecondAttack();
        // }
        

        // public void AttackEnd()
        // {
        //     player.HandleEndAttack();
        // }
        //
        // public void StartAnimationAttack()
        // {
        //     player.HandleStartAttack();
        // }

        // public void StartRangedAttack()
        // {
        //     player.HandleEndRangedAttack();
        // }
        //
        // public void EndRangedAttack()
        // {
        //     player.HandleStartRangedAttack();
        // }
        //
        // public void StartRangeSecondAttack()
        // {
        //     player.HandleStartRangeSecondAttack();
        // }
        //
        // public void EndRangeSecondAttack()
        // {
        //     player.HandleEndRangeSecondAttack();
        // }
        //
        // public void StartMoveForward()
        // {
        //     player.HandleStartMoveForward();
        // }
        //
        // public void EndMoveForward()
        // {
        //     player.HandleEndMoveForward();
        // }
        //
        // public void EndComboAttack()
        // {
        //     player.HandleEndComboAttack();
        // }
    }
}
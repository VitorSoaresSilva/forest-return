using UnityEngine;
namespace Enemies.StateMachine
{
    public class AttackState: BaseState
    {
        public override void UpdateState()
        {
            var distance = Vector3.Distance(owner.transform.position, owner._playerTransform.position);
            owner.navMeshAgent.destination = owner._playerTransform.position;
            if (!owner.isAttacking && distance < owner.enemyConfig.rangeAttack)
            {
                Attack();
            }
        }

        private void Attack()
        {
            owner.isAttacking = true;
            owner.navMeshAgent.isStopped = true;
            // animacao
            owner._animator.SetTrigger(owner.AnimatorAttackTrigger);
            owner.canAttack = false;
        }

        public override void DestroyState()
        {
            owner.isAttacking = false;
            owner.canCauseDamage = false;
        }

        public override void PrepareState()
        {
            owner.navMeshAgent.isStopped = false;
            owner.navMeshAgent.stoppingDistance = 1.5f;
        }
    }
}
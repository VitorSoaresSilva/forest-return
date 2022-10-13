
using UnityEngine;

namespace _Developers.Vitor.Scripts.Enemies.StateMachine
{
    public class ChasingState: BaseState
    {
        public override void UpdateState()
        {
            var needToMove = NeedToMove();
            // Debug.Log("Chasing " + needToMove);
            if (owner.canAttack)
            {
                owner.ChangeState(new AttackState());
            }
            else if (needToMove)
            {
                Move();
            }
            else
            {
                // Debug.Log("Has to stop");
                owner.navMeshAgent.isStopped = true;
            }
        }

        private void Move()
        {
            owner.navMeshAgent.isStopped = false;
            var distance = Vector3.Distance(owner.transform.position, owner._playerTransform.position);
            if (distance < owner.enemyConfig.minDistance)
            {
                var direction = (owner.transform.position - owner._playerTransform.position).normalized;
                owner.navMeshAgent.destination =
                    direction * Random.Range(owner.enemyConfig.minDistance, owner.enemyConfig.maxDistance);
            }
            else
            {
                owner.navMeshAgent.destination = owner._playerTransform.position;
            } 
            owner.navMeshAgent.destination = owner._playerTransform.position;
            
            
            
        }
        public override void PrepareState()
        {
            owner.navMeshAgent.isStopped = false;
            if (!owner.canAttack)
            {
                owner.cooldownAttackCoroutine = owner.StartCoroutine(nameof(owner.CooldownAttack));
            }
        }

        private bool NeedToMove()
        {
            var distance = Vector3.Distance(owner.transform.position, owner._playerTransform.position);
            var x = distance < owner.enemyConfig.minDistance || distance > owner.enemyConfig.maxDistance;
            return (x);
        }
    }
}
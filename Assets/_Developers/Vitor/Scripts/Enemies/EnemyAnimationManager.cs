using _Developers.Vitor.Scripts.Enemies.StateMachine;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Enemies
{
    public class EnemyAnimationManager : MonoBehaviour
    {
        private EnemyStateMachine _enemyStateMachine;

        private void Awake()
        {
            _enemyStateMachine = GetComponentInParent<EnemyStateMachine>();
        }

        public void EndAnimationAttack()
        {
            _enemyStateMachine.EndAnimationAttack();
        }

        public void StartAnimationAttack()
        {
            _enemyStateMachine.StartAnimationAttack();
        }

        public void EndAnimationDeath()
        {
            Destroy(_enemyStateMachine.gameObject);
        }
    }
}

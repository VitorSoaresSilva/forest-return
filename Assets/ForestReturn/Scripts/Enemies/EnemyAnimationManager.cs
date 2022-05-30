using System;
using Enemies.StateMachine;
using UnityEngine;

namespace Enemies
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
    }
}

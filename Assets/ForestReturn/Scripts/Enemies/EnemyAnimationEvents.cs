using System;
using UnityEngine;

namespace ForestReturn.Scripts.Enemies
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        private BaseEnemy _enemyRef;

        private void Awake()
        {
            _enemyRef = GetComponentInParent<BaseEnemy>();
        }

        public void EndAttackAnimation()
        {
            _enemyRef.HandleAnimationAttackEnded();
        }
        public void StartHitBox()
        {
            _enemyRef.HandleStartHitBox();
        }

        public void EndHitBox()
        {
            Debug.Log("end");
            _enemyRef.HandleEndHitBox();
        }
    }
}
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ForestReturn.Scripts.Enemies
{
    public class EnemyPlayerTrigger : MonoBehaviour
    {
        private BaseEnemy _enemyRef;

        private void Awake()
        {
            _enemyRef = gameObject.transform.parent.GetComponentInParent<BaseEnemy>();
        }

        private void OnTriggerStay(Collider other)
        {
            _enemyRef.PlayerDetected();
            gameObject.SetActive(false);
        }
    }
}
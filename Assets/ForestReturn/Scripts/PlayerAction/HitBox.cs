using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private int damage;
        private void OnTriggerStay(Collider other)
        {
            var damageable =  other.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
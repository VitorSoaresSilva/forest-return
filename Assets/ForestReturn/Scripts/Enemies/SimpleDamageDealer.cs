using System;
using Character;
using UnityEngine;

namespace Enemies
{
    public class SimpleDamageDealer : MonoBehaviour
    {
        public DataDamage dataDamage;

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.gameObject.transform.root.GetComponent<IDamageable>();
            damageable?.TakeDamage(dataDamage);
        }
    }
}
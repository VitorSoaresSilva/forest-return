using UnityEngine;

namespace ForestReturn.Scripts
{
    public class HitBox : MonoBehaviour
    {
        public int damage;
        private void OnTriggerStay(Collider other)
        {
            var damageable =  other.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
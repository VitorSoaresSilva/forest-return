using UnityEngine;

namespace ForestReturn.Scripts
{
    public class HitBox : MonoBehaviour
    {
        public int damage;
        private void OnTriggerStay(Collider other)
        {
            var damageable =  other.transform.root.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
using UnityEngine;

namespace ForestReturn.Scripts
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
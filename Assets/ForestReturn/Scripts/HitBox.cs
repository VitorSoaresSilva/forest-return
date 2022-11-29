using UnityEngine;

namespace ForestReturn.Scripts
{
    public class HitBox : MonoBehaviour
    {
        public int damage;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.transform.name);
            var damageable =  other.transform.root.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
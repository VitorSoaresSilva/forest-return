using UnityEngine;

namespace ForestReturn.Scripts
{
    public class SimpleDamageDealer : MonoBehaviour
    {
        public int damage;
        private void OnTriggerStay(Collider other)
        {
            var damageable = other.gameObject.transform.root.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
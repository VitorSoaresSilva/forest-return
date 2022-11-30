using UnityEngine;

namespace ForestReturn.Scripts
{
    public class SimpleContinuousDamageDealer : MonoBehaviour
    {
        public int damage;
        [SerializeField] protected float timeBetweenDamages = 1;
        protected bool _canDoDamage = true;
        
        
        private void OnTriggerStay(Collider other)
        {
            if (_canDoDamage)
            {
                _canDoDamage = false;
                var damageable = other.gameObject.transform.root.GetComponent<IDamageable>();
                damageable?.TakeDamage(damage);
                Invoke(nameof(EnableDamage),timeBetweenDamages);
            }
        }

        private void EnableDamage()
        {
            _canDoDamage = true;
        }
    }
}
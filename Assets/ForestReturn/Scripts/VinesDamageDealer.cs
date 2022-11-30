using UnityEngine;

namespace ForestReturn.Scripts
{
    public class VinesDamageDealer : SimpleContinuousDamageDealer
    {
        public float timeToReduceMoveSpeed = 0.5f;
        private void OnTriggerStay(Collider other)
        {
            
            if (_canDoDamage)
            {
                _canDoDamage = false;
                var damageable = other.gameObject.transform.root.GetComponent<IDamageable>();
                damageable?.TakeDamage(damage,false,timeToReduceMoveSpeed);
                Invoke(nameof(EnableDamage),timeBetweenDamages);
            }
        }

        private void EnableDamage()
        {
            _canDoDamage = true;
        }
    }
}
using UnityEngine;

namespace ForestReturn.Scripts
{
    public class SimpleContinuousDamageDealerIgnoreIntangibility : SimpleContinuousDamageDealer
    {
        private void OnTriggerStay(Collider other)
        {
            if (_canDoDamage)
            {
                _canDoDamage = false;
                var damageable = other.gameObject.transform.root.GetComponent<IDamageable>();
                damageable?.TakeDamage(damage,true,0);
                Invoke(nameof(EnableDamage),timeBetweenDamages);
            }
        }

        private void EnableDamage()
        {
            _canDoDamage = true;
        }
    }
}
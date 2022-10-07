using _Developers.Vitor.Scripts.Character;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Enemies
{
    public class SimpleDamageDealer : MonoBehaviour
    {
        public DataDamage dataDamage;

        private void OnTriggerStay(Collider other)
        {
            var damageable = other.gameObject.transform.root.GetComponent<IDamageable>();
            damageable?.TakeDamage(dataDamage);
        }
    }
}
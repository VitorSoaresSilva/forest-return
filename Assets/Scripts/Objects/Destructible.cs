using Character;
using UnityEngine;

namespace Objects
{
    public class Destructible : MonoBehaviour, IDamageable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public void TakeDamage(DataDamage dataDamage)
        {
            if (!TryGetComponent(out BaseCharacter baseCharacter))
            {
                Invoke(nameof(Die), 0.5f);
                if (_particleSystem != null)
                {
                    _particleSystem.Play();
                }
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
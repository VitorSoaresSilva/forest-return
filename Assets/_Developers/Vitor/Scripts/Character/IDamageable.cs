using UnityEngine;
namespace Character
{
    public interface IDamageable
    {
        public void TakeDamage(DataDamage dataDamage);
        public void TakeDamage(DataDamage dataDamage, Vector3 direction);
    }
}
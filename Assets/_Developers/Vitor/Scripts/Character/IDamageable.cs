using UnityEngine;

namespace _Developers.Vitor.Scripts.Character
{
    public interface IDamageable
    {
        public void TakeDamage(DataDamage dataDamage);
        public void TakeDamage(DataDamage dataDamage, Vector3 direction);
    }
}
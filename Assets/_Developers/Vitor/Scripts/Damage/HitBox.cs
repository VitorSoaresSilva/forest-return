using _Developers.Vitor.Scripts.Attacks;
using _Developers.Vitor.Scripts.Character;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Damage
{
    public class HitBox : MonoBehaviour
    {
        private BaseCharacter _baseCharacter;
        [SerializeField] private AttackScriptableObject attack;

        private void Start()
        {
            _baseCharacter = GetComponentInParent<BaseCharacter>();
        }

        private void OnTriggerStay(Collider other)
        {
            var damageable = other.GetComponentInParent<IDamageable>();
            var a = other.transform.position;
            var b = transform.position;
            a.y = b.y = 0;
            Vector3 direction = attack.knockBackForce.x * (a - b).normalized + attack.knockBackForce.y * Vector3.up;
            damageable?.TakeDamage(_baseCharacter.DataDamage,direction);
        }
    }
}
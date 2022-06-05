using System;
using Character;
using UnityEngine;

namespace Damage
{
    public class HitBox : MonoBehaviour
    {
        private BaseCharacter _baseCharacter;

        private void Start()
        {
            _baseCharacter = GetComponentInParent<BaseCharacter>();
        }

        private void OnTriggerStay(Collider other)
        {
            var damageable = other.GetComponentInParent<IDamageable>();
            Debug.Log("hitbox from " + gameObject.name + " hit" + other.name);
            damageable?.TakeDamage(_baseCharacter.DataDamage);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class BaseCharacter : MonoBehaviour, IDamageable
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentStamina { get; private set; }
        public int MaxStamina { get; private set; }
        public int Defense { get; private set; }
        public int Damage { get; private set; }
        public bool IsIntangible { get; private set; }
        public bool IsDead { get; private set; }
        [SerializeField] private float intangibleCoolDown = 0.5f;
        [SerializeField] private Attributes baseAttributes;

        public delegate void OnDeadEvent();
        public event OnDeadEvent OnDead;
        
        public delegate void OnHurtEvent();
        public event OnHurtEvent OnHurt;

        private void Awake()
        {
            CurrentHealth = MaxHealth = baseAttributes.health;
            MaxStamina = MaxStamina = baseAttributes.stamina;
            Defense = baseAttributes.defense;
            Damage = baseAttributes.damage;
        }

        public void TakeDamage(int damage)
        {
            TakeDamage(damage, Vector3.zero);
        }

        public void TakeDamage(int damage, Vector3 direction)
        {
            if (IsIntangible || IsDead) return;
            var damageTaken = Mathf.Max(damage - Defense, 0);
            if (damage <= 0) return;
            
            StartCoroutine(IntangibleCooldown());
            CurrentHealth -= damageTaken;
            OnHurt?.Invoke();
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                OnDead?.Invoke();
            }
        }
        private IEnumerator IntangibleCooldown()
        {
            //start shader
            IsIntangible = true;
            yield return new WaitForSeconds(intangibleCoolDown);
            IsIntangible = false;
            //stop shader
        }
    }
}
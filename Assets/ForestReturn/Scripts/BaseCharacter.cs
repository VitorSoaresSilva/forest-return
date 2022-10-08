using System.Collections;
using UnityEngine;

namespace ForestReturn.Scripts
{
    public class BaseCharacter : MonoBehaviour, IDamageable
    {
        private int _currentHealth;
        private int _currentMana;
        public int CurrentHealth
        {
            get => _currentHealth;
            protected set
            {
                var newValue = Mathf.Max(0, value);
                newValue = Mathf.Min(newValue, MaxHealth);
                _currentHealth = newValue;
            }
        }
        public int CurrentMana
        {
            get => _currentMana;
            protected set
            {
                var newValue = Mathf.Max(0, value);
                newValue = Mathf.Min(newValue, MaxMana);
                _currentMana = newValue; 
            }
        }
        public int MaxHealth { get; protected set; }
        public int MaxMana { get; private set; }
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
        public delegate void OnHealthHealedEvent();
        public event OnHealthHealedEvent OnHealthHealed;
        public delegate void OnManaHealedEvent();
        public event OnManaHealedEvent OnManaHealed;

        protected virtual void Awake()
        {
            MaxHealth = 10;
            CurrentHealth = MaxHealth;
            MaxMana = MaxMana = baseAttributes.mana;
            Defense = baseAttributes.defense;
            Damage = baseAttributes.damage;
        }

        public void TakeDamage(int damage)
        {
            if (IsIntangible || IsDead) return;
            Debug.Log("damage");
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

        protected void HealthHeal(int value)
        {
            CurrentHealth += value;
            OnHealthHealed?.Invoke();
        }

        protected void ManaHeal(int value)
        {
            CurrentMana += value;
            OnManaHealed?.Invoke();
        }
    }
}
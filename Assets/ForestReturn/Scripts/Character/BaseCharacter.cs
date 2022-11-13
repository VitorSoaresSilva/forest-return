using System.Collections;
using UnityEngine;

namespace ForestReturn.Scripts
{
    public struct BaseCharacterData
    {
        public int CurrentHealth;
        public int CurrentMana;
    }
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
                OnLifeChanged?.Invoke();
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
                OnManaChanged?.Invoke();
            }
        }
        public int MaxHealth { get; protected set; }
        public int MaxMana { get; private set; }
        public int Defense { get; private set; }
        public int Damage { get; private set; }
        public bool IsIntangible { get; protected set; }
        public bool IsDead { get; private set; }
        public bool IsDefending { get; protected set; }
        [SerializeField] private float intangibleCoolDown = 0.5f;
        [SerializeField] private Attributes baseAttributes;

        public delegate void OnDeadEvent();
        public delegate void OnHurtEvent(int damage);
        public delegate void OnHealthHealedEvent(int oldValue, int newValue);
        public delegate void OnManaHealedEvent();
        public delegate void OnLifeChangeEvent();
        public delegate void OnManaChangeEvent();
        public event OnDeadEvent OnDead;
        public event OnHurtEvent OnHurt;
        public event OnHealthHealedEvent OnHealthHealed;
        public event OnManaHealedEvent OnManaHealed;
        public event OnLifeChangeEvent OnLifeChanged;
        public event OnManaChangeEvent OnManaChanged;
        

        protected virtual void Awake()
        {
            MaxHealth = baseAttributes.health;
            CurrentHealth = MaxHealth;
            MaxMana = MaxMana = baseAttributes.mana;
            CurrentMana = MaxMana;
            Defense = baseAttributes.defense;
            Damage = baseAttributes.damage;
        }

        public void TakeDamage(int damage)
        {
            if (IsIntangible || IsDead) return;
            var damageTaken = IsDefending ? Mathf.Max(damage - Defense, 0) : damage;
            if (damageTaken <= 0) return;
            StartCoroutine(IntangibleCooldown());
            CurrentHealth -= damageTaken;
            OnHurt?.Invoke(damageTaken);
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
            int oldValue = CurrentHealth;
            CurrentHealth += value;
            OnHealthHealed?.Invoke(oldValue, CurrentHealth);
        }

        protected void ManaHeal(int value)
        {
            CurrentMana += value;
            OnManaHealed?.Invoke();
        }
    }
}
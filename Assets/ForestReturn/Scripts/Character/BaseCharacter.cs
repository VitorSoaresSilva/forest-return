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
        private int _maxHealth;
        private int _maxMana;
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

        public int MaxHealth
        {
            get => _maxHealth;
            protected set
            {
                _maxHealth = Mathf.Max(0, value);
                OnMaxHealthChanged?.Invoke();
            }
        }

        public int MaxMana
        {
            get => _maxMana;
            protected set
            {
                _maxMana = Mathf.Max(0, value);
                OnMaxManaChanged?.Invoke();
            }
        }

        public int Defense { get; private set; }
        public int Damage { get; private set; }
        public bool IsIntangible { get; protected set; }
        public bool IsDead { get; private set; }
        public bool IsDefending { get; protected set; }
        [SerializeField] private float intangibleCoolDown = 0.5f;
        [SerializeField] protected Attributes baseAttributes;

        public delegate void OnDeadEvent();
        public delegate void OnHurtEvent(int damage);
        public delegate void OnHealthHealedEvent(int oldValue, int newValue);
        public delegate void OnLifeChangeEvent();
        public delegate void OnManaHealedEvent();
        public delegate void OnManaChangeEvent();
        public delegate void OnNotEnoughManaEvent();
        public delegate void OnMaxHealthChangedEvent();
        public delegate void OnMoveSpeedReducedEvent();
        public delegate void OnMoveSpeedNormalizedEvent();
        public delegate void OnMaxManaChangedEvent();
        public event OnDeadEvent OnDead;
        public event OnHurtEvent OnHurt;
        public event OnHealthHealedEvent OnHealthHealed;
        public event OnMaxHealthChangedEvent OnMaxHealthChanged;
        public event OnMaxManaChangedEvent OnMaxManaChanged;
        public event OnManaHealedEvent OnManaHealed;
        public event OnLifeChangeEvent OnLifeChanged;
        public event OnManaChangeEvent OnManaChanged;
        public event OnNotEnoughManaEvent OnNotEnoughMana;
        public event OnMoveSpeedReducedEvent OnMoveSpeedReduced;
        public event OnMoveSpeedNormalizedEvent OnMoveSpeedNormalized;
        

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
        public void TakeDamage(int damage,bool ignoreIntangibility, float timeToReduceMoveSpeed)
        {
            if ((!ignoreIntangibility && IsIntangible) || IsDead) return;
            var damageTaken = IsDefending ? Mathf.Max(damage - Defense, 0) : damage;
            if (damageTaken <= 0) return;
            StartCoroutine(IntangibleCooldown());
            CurrentHealth -= damageTaken;
            OnHurt?.Invoke(damageTaken);
            if (timeToReduceMoveSpeed > 0)
            {
                OnMoveSpeedReduced?.Invoke();
            }
            StartCoroutine(MoveSpeedReducedCooldown(timeToReduceMoveSpeed));
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                OnDead?.Invoke();
            }
        }

        private IEnumerator MoveSpeedReducedCooldown(float time)
        {
            yield return new WaitForSeconds(time);
            OnMoveSpeedNormalized?.Invoke();
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

        protected bool UseMana(int value = 1)
        {
            if (CurrentMana >= value)
            {
                CurrentMana -= value;
                return true;
            }
            else
            {
                OnNotEnoughMana?.Invoke();
                return false;
            }
        }
    }
}
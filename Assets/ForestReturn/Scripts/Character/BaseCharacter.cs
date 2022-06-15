using System;
using System.Collections;
using System.Reflection;
using Artifacts;
using UnityEngine;
using Attributes;
using Weapons;
using Attribute = Attributes.Attribute;

namespace Character
{
    public class BaseCharacter : MonoBehaviour, IDamageable
    {
        public Attribute[] attributes;
        [SerializeField] private CharacterStatScriptableObject characterStats; 
        [SerializeField] private AttributeModifier[] baseModifiers;
        protected bool isIntangible = false;
        public bool isDead { get; private set; }
        // public Weapon Weapon { get; protected set; }
        // public WeaponsScriptableObject initialWeaponData;
        // public ArtifactsScriptableObject[] initialArtifactsToWeapon;
        protected Rigidbody _rigidbody;

        public delegate void OnDeadEvent();
        public event OnDeadEvent OnDead;

        protected delegate void OnHurEvent(Vector3 knockBackForce);
        protected event OnHurEvent OnHurt;
        public DataDamage DataDamage
        {
            get => new (attributes[(int)AttributeType.Attack].CurrentValue,
                attributes[(int)AttributeType.TrueDamageAttack].CurrentValue);
        }

        public int CurrentHealth
        {
            get => attributes[(int) AttributeType.Health].CurrentValue;
            private set => attributes[(int) AttributeType.Health].CurrentValue = value;
        }
        public int CurrentStamina
        {
            get => attributes[(int) AttributeType.Stamina].CurrentValue;
            private set => attributes[(int) AttributeType.Stamina].CurrentValue = value;
        }
        public int CurrentMana
        {
            get => attributes[(int) AttributeType.Mana].CurrentValue;
            private set => attributes[(int) AttributeType.Mana].CurrentValue = value;
        }

        protected virtual void Awake()
        {
            attributes = new Attribute[(int) AttributeType.COUNT];
            _rigidbody = GetComponent<Rigidbody>();
            // Create the Attributes based on enum: AttributeType
            for (int i = 0; i < (int)AttributeType.COUNT; ++i)
            {
                int value = 0;
                var field = typeof(CharacterStatScriptableObject).GetField(
                    Enum.GetName(typeof(AttributeType), (AttributeType) i)!, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly  | BindingFlags.IgnoreCase);
                if (field!=null)
                {
                    value = (int) (field.GetValue(characterStats) ?? 0);
                }
                attributes[i] = new Attribute(value,(AttributeType)i);
            }
            // Add base modifiers set on Inspector
            for (int i = 0; i < baseModifiers.Length; i++)
            {
                attributes[(int)baseModifiers[i].type].AddModifier(baseModifiers[i].value);
            }

            // Weapon = initialArtifactsToWeapon == null ? 
            //     new Weapon(this, initialWeaponData) : 
            //     new Weapon(this, initialWeaponData, initialArtifactsToWeapon);
        }
        
        public void TakeDamage(DataDamage dataDamage)
        {
            TakeDamage(dataDamage, Vector3.zero);
        }

        public void TakeDamage(DataDamage dataDamage, Vector3 direction)
        {
            if (isIntangible || isDead) return; 
            
            int damageTaken = dataDamage.damage - attributes[(int) AttributeType.Armor].MaxValue;
            damageTaken = Mathf.Max(damageTaken, 0);
            var damage = (damageTaken + dataDamage.trueDamage);
            if (damage > 0)
            {
                isIntangible = true;
                CurrentHealth -= damage;
                StartCoroutine(nameof(IntangibleCooldown));
                OnHurt?.Invoke(direction);
                if (CurrentHealth <= 0)
                {
                    isDead = true;
                    OnDead?.Invoke();
                }
            }
        }

        private IEnumerator IntangibleCooldown()
        {
            yield return new WaitForSeconds(2);
            isIntangible = false;
        }
    }
}
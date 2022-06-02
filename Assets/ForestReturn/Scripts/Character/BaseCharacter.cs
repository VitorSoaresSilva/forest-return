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
        public Weapon weapon { get; protected set; }
        public WeaponsScriptableObject initialWeaponData;
        public ArtifactsScriptableObject[] initialArtifactsToWeapon;

        public DataDamage DataDamage
        {
            get => new DataDamage(attributes[(int)AttributeType.Attack].CurrentValue,
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

            weapon = (initialArtifactsToWeapon == null || initialArtifactsToWeapon.Length == 0)
                ? new Weapon(this, initialWeaponData)
                : new Weapon(this, initialWeaponData, initialArtifactsToWeapon); 
            
        }
        
        public void TakeDamage(DataDamage dataDamage)
        {
            if (isIntangible) return;
            int damageTaken = dataDamage.damage - attributes[(int) AttributeType.Armor].MaxValue;
            damageTaken = Mathf.Max(damageTaken, 0);
            var damage = (damageTaken + dataDamage.trueDamage);
            Debug.Log(damage);
            if (damage > 0)
            {
                CurrentHealth -= damage;
                StartCoroutine(nameof(IntangibleCooldown));
                isIntangible = true;
                if (CurrentHealth <= 0)
                {
                    Debug.Log("Dead");
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
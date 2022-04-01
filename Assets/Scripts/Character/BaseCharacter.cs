using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Attributes;
using DefaultNamespace.UI;
using Interactable;
using UnityEngine;
using Weapons;
using Attribute = Attributes.Attribute;

namespace Character 
{
    // [RequireComponent(typeof(WeaponManager))]
    public class BaseCharacter : MonoBehaviour, IDamageable
    {
        public Attribute[] attributes;
        public CharacterStatScriptableObject characterStats; //flag 
        public AttributeModifier[] _baseModifiers;
        [SerializeField] private int CurrentHealth = 0;
        [SerializeField] private int CurrentMana = 0;
        [SerializeField] private int CurrentStamina = 0;
        [SerializeField] private int CurrentDamage = 0;
        [SerializeField] private int CurrentTrueDamage = 0;
        private WeaponManager _weaponManager;

        public UiManager UiManager;
        // [SerializeField] private Animator _animator;
        
        private void Awake()    
        {
            attributes = new Attribute[(int) AttributeType.COUNT];
            for (int i = 0; i < (int)AttributeType.COUNT; ++i)
            {
                int value = 0;
                var field = typeof(CharacterStatScriptableObject).GetField(
                    Enum.GetName(typeof(AttributeType), (AttributeType) i), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly  | BindingFlags.IgnoreCase);
                if (field!=null)
                {
                    value = (int) (field.GetValue(characterStats) ?? 0);
                }
                attributes[i] = new Attribute(value);
            }
            for (int i = 0; i < _baseModifiers.Length; i++)
            {
                attributes[(int)_baseModifiers[i].type].AddModifier(_baseModifiers[i].value);
            }
            _weaponManager = GetComponent<WeaponManager>();
            attributes[(int) AttributeType.Health].changedValue.AddListener(HandleHealthChanged);
            attributes[(int) AttributeType.Mana].changedValue.AddListener(HandleManaChanged);
            attributes[(int) AttributeType.Stamina].changedValue.AddListener(HandleStaminaChanged);
            attributes[(int) AttributeType.Attack].changedValue.AddListener(HandleDamageChanged);
            attributes[(int) AttributeType.TrueDamageAttack].changedValue.AddListener(HandleTrueDamageChanged);
           
            HandleHealthChanged();
            HandleManaChanged();
            HandleStaminaChanged();
            HandleTrueDamageChanged();
            HandleDamageChanged();
            
        }

        public void TakeDamage(DataDamage dataDamage)
        {
            int damageTaken = dataDamage.damage - attributes[(int) AttributeType.Armor].CurrentValue;
            damageTaken = Mathf.Max(damageTaken, 0);
            CurrentHealth -= (damageTaken + dataDamage.trueDamage);
            if (CurrentHealth <= 0)
            {
                Debug.Log("Dead");
            }
        }

        public void HandleHealthChanged()
        {
            CurrentHealth = attributes[(int) AttributeType.Health].CurrentValue;
            UiManager.health.text = "Health :" + CurrentHealth;
        }
        public void HandleManaChanged()
        {
            CurrentMana = attributes[(int) AttributeType.Mana].CurrentValue;
            UiManager.mana.text = "Mana :" + CurrentMana;
        }
        public void HandleStaminaChanged()
        {
            CurrentStamina = attributes[(int) AttributeType.Stamina].CurrentValue;
            UiManager.stamina.text = "Stamina :" + CurrentStamina;
        }
        public void HandleDamageChanged()
        {
            CurrentDamage = attributes[(int) AttributeType.Attack].CurrentValue;
            UiManager.damage.text = "Damage :" + CurrentDamage;
        }
        public void HandleTrueDamageChanged()
        {
            CurrentTrueDamage = attributes[(int) AttributeType.TrueDamageAttack].CurrentValue;
            UiManager.trueDamage.text = "True Damage :" + CurrentTrueDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable component))
            {
                component.TakeDamage(_weaponManager.DataDamage);
            }else if (other.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
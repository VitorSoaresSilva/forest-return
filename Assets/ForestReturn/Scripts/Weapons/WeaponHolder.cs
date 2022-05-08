using System;
using Character;
using UnityEngine;
using System.Collections.Generic;
using Artifacts;
using Attributes;
using DefaultNamespace.UI;
using Utilities;
using Attribute = Attributes.Attribute;

namespace Weapons
{
    [RequireComponent(typeof(BaseCharacter))]
    public class WeaponHolder : MonoBehaviour
    {
        public WeaponsScriptableObject initialWeaponData;
        private Weapon _weapon;
        private BaseCharacter _baseCharacter;
        public Weapon Weapon { get =>  _weapon;
            set => _weapon = value;
        }
        // private DataDamage _dataDamage;
        public DataDamage DataDamage {  
            get => new(_baseCharacter.attributes[(int)AttributeType.Attack].CurrentValue,
                _baseCharacter.attributes[(int)AttributeType.TrueDamageAttack].CurrentValue);
        }
        protected void Awake()
        {
            _baseCharacter = GetComponent<BaseCharacter>();
            _weapon = new Weapon(initialWeaponData.slotsAmount);
            int initialArtifactsAmount = Mathf.Min(initialWeaponData.initialArtifacts.Length, initialWeaponData.slotsAmount);
            for (int i = 0; i < initialArtifactsAmount; i++)
            {
                AddArtifact(initialWeaponData.initialArtifacts[i],i);
            }
        }

        public void AddArtifact(ArtifactsScriptableObject data,int slotIndex)
        {
            if (_weapon.artifacts[slotIndex] != null)
            {
                RemoveArtifact(slotIndex);
            }
            _weapon.artifacts[slotIndex] = data;
            foreach (var modifier in data.modifiers)
            {
                _baseCharacter.attributes[(int)modifier.type].AddModifier(modifier.value);
            }
        }
        public void RemoveArtifact(int slotIndex)
        {
            if (_weapon.artifacts[slotIndex] == null) return;
            foreach (var modifier in _weapon.artifacts[slotIndex].modifiers)
            {
                _baseCharacter.attributes[(int)modifier.type].RemoveModifier(modifier.value);
            }
        }
    }
} 
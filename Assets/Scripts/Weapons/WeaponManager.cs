using System;
using Character;
using UnityEngine;
using System.Collections.Generic;
using Artifacts;
using Attributes;
using Utilities;

namespace Weapons
{
    public class WeaponManager : Singleton<WeaponManager>
    {
        public WeaponsScriptableObject initialWeaponData;
        private Weapon _weapon;
        public BaseCharacter _baseCharacter;
        public Weapon weapon { get =>  _weapon;
            set => _weapon = value;
        }
        private DataDamage _dataDamage;
        public DataDamage DataDamage {  get => _dataDamage;
            private set => _dataDamage = value;
        }

        protected override void Awake()
        {
            base.Awake();
            _baseCharacter = GetComponent<BaseCharacter>();
            _weapon = new Weapon(initialWeaponData.slotsAmount);
            int initialArtifactsAmount = Mathf.Min(initialWeaponData.initialArtifacts.Length, initialWeaponData.slotsAmount);
            for (int i = 0; i < initialArtifactsAmount; i++)
            {
                _weapon.AddArtifact(initialWeaponData.initialArtifacts[i],i);
            }
            _baseCharacter.UiManager.ActiveSlots(weapon.artifacts.Length);
            UpdateDataDamage();
        }

        public void AddModifierFromArtifactToBase(AttributeModifier data)
        {
            _baseCharacter.attributes[(int)data.type].AddModifier(data.value);
        }
        public void RemoveModifierFromArtifactToBase(AttributeModifier data)
        {
            _baseCharacter.attributes[(int)data.type].RemoveModifier(data.value);
        }
 
        public void UpdateDataDamage()
        {
            DataDamage = new DataDamage(0,0);
            foreach (var artifact in _weapon.artifacts)
            {
                if (artifact == null) continue;
                foreach (var modifier in artifact.modifiers)
                {
                    switch (modifier.type)
                    {
                        case AttributeType.Attack:
                            _dataDamage.damage += modifier.value;
                            break;
                        case AttributeType.TrueDamageAttack:
                            _dataDamage.trueDamage += modifier.value;
                            break;
                    }
                }
            }
            _dataDamage.damage += _baseCharacter.characterStats.attack;
            _dataDamage.trueDamage += _baseCharacter.characterStats.trueDamageAttack;
        }
    }
} 
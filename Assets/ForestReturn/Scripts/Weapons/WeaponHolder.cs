using System;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Artifacts;
using Attributes;
using UI;

namespace Weapons
{
    [RequireComponent(typeof(BaseCharacter))]
    public class WeaponHolder : MonoBehaviour
    {
        private Weapon _weapon;
        private BaseCharacter _baseCharacter;
        private List<ArtifactsScriptableObject> _artifactsNotInUse;
        
        [Header("Initial Data Weapon")]
        [SerializeField] private WeaponsScriptableObject initialWeapon;
        [SerializeField] private ArtifactsScriptableObject[] initialArtifacts;
        private void Start()
        {
            _weapon = null;
            _baseCharacter = GetComponent<BaseCharacter>();
            _artifactsNotInUse = new List<ArtifactsScriptableObject>();
            
            Weapon weapon = new Weapon(initialWeapon);
            _artifactsNotInUse.AddRange(initialArtifacts);
            EquipWeapon(weapon);
            var length = _artifactsNotInUse.Count;
            for (int i = 0; i < length; i++)
            {
                TryEquipArtifactFromInventory(0);
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            
            if (weapon == null) return;
            if (_weapon != null)
            {
                RemoveWeapon();
            }
            _weapon = weapon;
            _baseCharacter.attributes[(int)AttributeType.Attack].AddModifier(_weapon.weaponConfig.DataDamage.damage);
            _baseCharacter.attributes[(int)AttributeType.TrueDamageAttack].AddModifier(_weapon.weaponConfig.DataDamage.trueDamage);
            foreach (var artifactsScriptableObject in _weapon.artifacts)
            {
                if (artifactsScriptableObject != null)
                {
                    foreach (var attributeModifier in artifactsScriptableObject.modifiers)
                    {
                        _baseCharacter.attributes[(int)attributeModifier.type].AddModifier(attributeModifier.value);
                    }
                }
            }
            // TODO: Trigger Event for UiManager
        }

        public void RemoveWeapon()
        {
            if (_weapon == null) return;
            _baseCharacter.attributes[(int)AttributeType.Attack].RemoveModifier(_weapon.weaponConfig.DataDamage.damage);
            _baseCharacter.attributes[(int)AttributeType.TrueDamageAttack].RemoveModifier(_weapon.weaponConfig.DataDamage.trueDamage);
            foreach (var artifactsScriptableObject in _weapon.artifacts)
            {
                foreach (var attributeModifier in artifactsScriptableObject.modifiers)
                {
                    _baseCharacter.attributes[(int)attributeModifier.type].RemoveModifier(attributeModifier.value);
                }
                _artifactsNotInUse.Add(artifactsScriptableObject);
            }
            _weapon = null;
            // TODO: Trigger an Event to UiManager
        }

        public void CollectArtifact(ArtifactsScriptableObject newArtifact)
        {
            if (newArtifact != null)
            {
                _artifactsNotInUse.Add(newArtifact);
            }
        }

        public void TryEquipArtifactFromInventory(int index)
        {
            if (index >= _artifactsNotInUse.Count) return;
            bool hasEmptySlots = false;
            int indexEmptySlot = -1;
            for (int i = 0; i < _weapon.artifacts.Length; i++)
            {
                if (hasEmptySlots || _weapon.artifacts[i] != null) continue;
                hasEmptySlots = true;
                indexEmptySlot = i;
            }
            if (!hasEmptySlots)
            {
                Debug.Log("Not Enough Space");
                return;
            }
            ArtifactsScriptableObject artifact = _artifactsNotInUse[index];
            _artifactsNotInUse.RemoveAt(index);
            _weapon.artifacts[indexEmptySlot] = artifact;
            foreach (var attributeModifier in artifact.modifiers)
            {
                _baseCharacter.attributes[(int)attributeModifier.type].RemoveModifier(attributeModifier.value);
            }
        }
    }
} 
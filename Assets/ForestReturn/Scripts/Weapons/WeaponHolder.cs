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
        public Weapon Weapon { get; private set; }
        private BaseCharacter _baseCharacter;
        private List<ArtifactsScriptableObject> _artifactsNotInUse;
        private List<WeaponsScriptableObject> _weaponsNotInUse;
        
        [Header("Initial Data Weapon")]
        [SerializeField] private WeaponsScriptableObject initialWeapon;
        [SerializeField] private ArtifactsScriptableObject[] initialArtifacts;
        private void Start()
        {
            _baseCharacter = GetComponent<BaseCharacter>();
            _artifactsNotInUse = new List<ArtifactsScriptableObject>();
            _weaponsNotInUse = new List<WeaponsScriptableObject>();
            // Weapon weapon = new Weapon(initialWeapon);
            if (initialWeapon != null)
            {
                _weaponsNotInUse.Add(initialWeapon);
            }

            if (initialArtifacts.Length > 0)
            {
                _artifactsNotInUse.AddRange(initialArtifacts);
            }
            EquipWeapon(0);
            var length = _artifactsNotInUse.Count;
            for (int i = 0; i < length; i++)
            {
                TryEquipArtifactFromInventory(0);
            }
        }
        public void EquipWeapon(int index)
        {
            var weaponsScriptableObject = _weaponsNotInUse[index];   
            if (weaponsScriptableObject == null) return;
            if (Weapon != null)
            {
                RemoveWeapon();
            }
            _weaponsNotInUse.RemoveAt(index);
            Weapon = new Weapon(weaponsScriptableObject);
            _baseCharacter.attributes[(int)AttributeType.Attack].AddModifier(Weapon.weaponConfig.DataDamage.damage);
            _baseCharacter.attributes[(int)AttributeType.TrueDamageAttack].AddModifier(Weapon.weaponConfig.DataDamage.trueDamage);
            // TODO: Trigger Event for UiManager
        }

        public void RemoveWeapon()
        {
            if (Weapon == null) return;
            _baseCharacter.attributes[(int)AttributeType.Attack].RemoveModifier(Weapon.weaponConfig.DataDamage.damage);
            _baseCharacter.attributes[(int)AttributeType.TrueDamageAttack].RemoveModifier(Weapon.weaponConfig.DataDamage.trueDamage);
            foreach (var artifactsScriptableObject in Weapon.artifacts)
            {
                if (artifactsScriptableObject != null)
                {
                    foreach (var attributeModifier in artifactsScriptableObject.modifiers)
                    {
                        _baseCharacter.attributes[(int)attributeModifier.type].RemoveModifier(attributeModifier.value);
                    }
                    _artifactsNotInUse.Add(artifactsScriptableObject);
                }
            }
            _weaponsNotInUse.Add(Weapon.weaponConfig);
            Weapon = null;
            // TODO: Trigger an Event to UiManager
        }

        

        public void TryEquipArtifactFromInventory(int index)
        {
            if (index >= _artifactsNotInUse.Count) return;
            bool hasEmptySlots = false;
            int indexEmptySlot = -1;
            for (int i = 0; i < Weapon.artifacts.Length; i++)
            {
                if (hasEmptySlots || Weapon.artifacts[i] != null) continue;
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
            Weapon.artifacts[indexEmptySlot] = artifact;
            foreach (var attributeModifier in artifact.modifiers)
            {
                _baseCharacter.attributes[(int)attributeModifier.type].AddModifier(attributeModifier.value);
            }
        }

        public void CollectWeapon(WeaponsScriptableObject newWeapon)
        {
            if (newWeapon != null)
            {
                _weaponsNotInUse.Add(newWeapon);
            }
        }
        public void CollectArtifact(ArtifactsScriptableObject newArtifact)
        {
            if (newArtifact != null)
            {
                _artifactsNotInUse.Add(newArtifact);
                if (UiManager.instance != null)
                {
                    UiManager.instance.ShowArtifact(newArtifact);
                }
            }
        }

        public List<WeaponsScriptableObject> GetWeapons()
        {
            return _weaponsNotInUse;
        }
    }
} 
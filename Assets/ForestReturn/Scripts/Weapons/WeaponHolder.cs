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
        public List<ArtifactsScriptableObject> ArtifactsInventory { get; private set; }
        public List<WeaponsScriptableObject> WeaponsInventory { get; private set; }

        [Header("Initial Data Weapon")]
        [SerializeField] private WeaponsScriptableObject initialWeapon;
        [SerializeField] private ArtifactsScriptableObject[] initialArtifacts;
        private void Start()
        {
            _baseCharacter = GetComponent<BaseCharacter>();
            ArtifactsInventory = new List<ArtifactsScriptableObject>();
            WeaponsInventory = new List<WeaponsScriptableObject>();
            if (initialWeapon != null)
            {
                WeaponsInventory.Add(initialWeapon);
            }
            if (initialArtifacts.Length > 0)
            {
                ArtifactsInventory.AddRange(initialArtifacts);
            }
            EquipWeapon(0);
            var length = ArtifactsInventory.Count;
            for (int i = 0; i < length; i++)
            {
                TryEquipArtifactFromInventory(0);
            }
        }
        public void EquipWeapon(int index)
        {
            var weaponsScriptableObject = WeaponsInventory[index];   
            if (weaponsScriptableObject == null) return;
            if (Weapon != null)
            {
                RemoveWeapon();
            }
            WeaponsInventory.RemoveAt(index);
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
                    ArtifactsInventory.Add(artifactsScriptableObject);
                }
            }
            WeaponsInventory.Add(Weapon.weaponConfig);
            Weapon = null;
            // TODO: Trigger an Event to UiManager
        }

        

        public void TryEquipArtifactFromInventory(int index)
        {
            if (index >= ArtifactsInventory.Count) return;
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
            ArtifactsScriptableObject artifact = ArtifactsInventory[index];
            ArtifactsInventory.RemoveAt(index);
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
                WeaponsInventory.Add(newWeapon);
                UiManager.instance.ShowCollectedWeapon(newWeapon);
            }
        }
        public void CollectArtifact(ArtifactsScriptableObject newArtifact)
        {
            if (newArtifact != null)
            {
                ArtifactsInventory.Add(newArtifact);
                if (UiManager.instance != null)
                {
                    UiManager.instance.ShowArtifact(newArtifact);
                }
            }
        }
    }
} 
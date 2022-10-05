using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;
using Artifacts;
using Attributes;
using ForestReturn.Scripts.Data;
using Managers;
using UI;
using Utilities;

namespace Weapons
{
    [RequireComponent(typeof(BaseCharacter))]
    public class WeaponHolder : MonoBehaviour
    {
        public Weapon Weapon { get; private set; }
        private BaseCharacter _baseCharacter;
        public List<ArtifactsScriptableObject> ArtifactsInventory { get; private set; }
        public List<WeaponsScriptableObject> WeaponsInventory { get; private set; }

        // [Header("Initial Data Weapon")]
        // [SerializeField] private WeaponsScriptableObject initialWeapon;
        // [SerializeField] private ArtifactsScriptableObject[] initialArtifacts;
        private void Start()
        {
            _baseCharacter = GetComponent<BaseCharacter>();
            LoadInventory();
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
            SaveInventory();
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
            SaveInventory();
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

            SaveInventory();
        }

        public void CollectWeapon(WeaponsScriptableObject newWeapon, bool showUI = true)
        {
            if (newWeapon != null)
            {
                WeaponsInventory.Add(newWeapon);

                if (showUI)
                {
                    UiManager.instance.ShowCollectedWeapon(newWeapon);
                }
                SaveInventory();
            }
        }
        public void CollectArtifact(ArtifactsScriptableObject newArtifact, bool showUI = true)
        {
            if (newArtifact != null)
            {
                ArtifactsInventory.Add(newArtifact);
                SaveInventory();
                if (showUI)
                {
                    if (UiManager.instance != null)
                    {
                        UiManager.instance.ShowArtifact(newArtifact);
                    }
                }
            }
        }

        public void LoadInventory()
        {
            if (GameManager.instance.InventoryScriptableObject.ArtifactsInventory.Length > 0)
            {
                ArtifactsInventory = GameManager.instance.InventoryScriptableObject.ArtifactsInventory.ToList();
            }
            else
            {
                ArtifactsInventory = new List<ArtifactsScriptableObject>();
            }

            if (GameManager.instance.InventoryScriptableObject.WeaponsInventory.Length > 0)
            {
                WeaponsInventory = GameManager.instance.InventoryScriptableObject.WeaponsInventory.ToList();
            }
            else
            {
                WeaponsInventory = new List<WeaponsScriptableObject>();
            }
            
            if (GameManager.instance.InventoryScriptableObject.WeaponEquiped != null)
            {
                CollectWeapon(GameManager.instance.InventoryScriptableObject.WeaponEquiped,false);
                EquipWeapon(WeaponsInventory.Count-1);
            }
            for (int i = 0; i < GameManager.instance.InventoryScriptableObject.ArtifactsEquiped.Length; i++)
            {
                CollectArtifact(GameManager.instance.InventoryScriptableObject.ArtifactsEquiped[i],false);
                if (ArtifactsInventory.Count > 0)
                {
                    TryEquipArtifactFromInventory(ArtifactsInventory.Count - 1);
                }
            }
        }

        public void SaveInventory()
        {
            if (Weapon == null)
            {
                Debug.Log("cagou");
                GameManager.instance.InventoryScriptableObject.WeaponEquiped = null;
            }
            else
            {
                GameManager.instance.InventoryScriptableObject.WeaponEquiped = Weapon.weaponConfig;
            }

            // if (GameManager.instance.InventoryScriptableObject.ArtifactsEquiped.Length > 0)
            // {
            //     GameManager.instance.InventoryScriptableObject.ArtifactsEquiped = Weapon.artifacts;
            //     
            // }
            //     GameManager.instance.InventoryScriptableObject.ArtifactsEquiped = null;
            GameManager.instance.InventoryScriptableObject.ArtifactsInventory = ArtifactsInventory.ToArray();
            GameManager.instance.InventoryScriptableObject.WeaponsInventory = WeaponsInventory.ToArray();
        }
    }
} 
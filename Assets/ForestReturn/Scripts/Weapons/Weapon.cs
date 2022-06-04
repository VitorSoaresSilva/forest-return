using Artifacts;
using Character;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class Weapon
    {
        public ArtifactsScriptableObject[] artifacts;
        public WeaponsScriptableObject weaponConfig;
        private BaseCharacter _owner;
        public Weapon(BaseCharacter owner, WeaponsScriptableObject initialWeaponConfig)
        {
            weaponConfig = initialWeaponConfig;
            artifacts = new ArtifactsScriptableObject[initialWeaponConfig.slotsAmount];
            _owner = owner;
        }
        public Weapon(BaseCharacter owner,WeaponsScriptableObject initialWeaponConfig, ArtifactsScriptableObject[] artifacts )
        {
            _owner = owner;
            weaponConfig = initialWeaponConfig;
            this.artifacts = new ArtifactsScriptableObject[initialWeaponConfig.slotsAmount];
            for (int i = 0; i < artifacts.Length; i++)
            {
                AddArtifact(artifacts[i],i);
            }
        }
        public void AddArtifact(ArtifactsScriptableObject artifactsScriptableObject, int slotIndex)
        {
            if (artifacts[slotIndex] != null)
            {
                RemoveArtifact(slotIndex);
            }

            artifacts[slotIndex] = artifactsScriptableObject;
            foreach (var modifier in artifactsScriptableObject.modifiers)
            {
                _owner.attributes[(int)modifier.type].AddModifier(modifier.value);
            }
        }

        public void RemoveArtifact(int slotIndex)
        {
            if (artifacts[slotIndex] == null) return;
            foreach (var modifier in artifacts[slotIndex].modifiers)
            {
                _owner.attributes[(int)modifier.type].RemoveModifier(modifier.value);
            }
            artifacts[slotIndex] = null;
        }
    }
}
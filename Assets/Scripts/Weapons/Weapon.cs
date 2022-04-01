using Artifacts;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class Weapon
    {
        public ArtifactsScriptableObject[] artifacts;
        public Weapon(int slotsAmount)
        {
            artifacts = new ArtifactsScriptableObject[slotsAmount];
        }

        public void AddArtifact(ArtifactsScriptableObject data,int slotIndex)
        {
            if (artifacts.Length > slotIndex)
            {
                artifacts[slotIndex] = data;
            }
        }
        public bool AddArtifact(ArtifactsScriptableObject data)
        {
            for (int i = 0; i < artifacts.Length; i++)
            {
                if (artifacts[i] == null)
                {
                    artifacts[i] = data;
                    if (WeaponManager.InstanceExists)
                    {
                        WeaponManager.Instance.UpdateDataDamage();
                        foreach (var attributeModifier in data.modifiers)
                        {
                            WeaponManager.Instance.AddModifierFromArtifactToBase(attributeModifier);
                        }

                        WeaponManager.Instance._baseCharacter.UiManager.slots[i].sprite = data.model2d;

                    }
                    return true;
                }
            }
            return false;
        }
        public void RemoveArtifact(int slotIndex)
        {
            if (artifacts.Length > slotIndex)
            {
                artifacts[slotIndex] = null;
            }
        }
        
    }
}
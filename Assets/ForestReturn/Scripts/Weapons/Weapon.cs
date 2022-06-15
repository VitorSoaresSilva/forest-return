using Artifacts;
using Character;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class Weapon
    {
        public WeaponsScriptableObject weaponConfig;
        public ArtifactsScriptableObject[] artifacts;
        public Weapon(WeaponsScriptableObject initialWeaponConfig)
        {
            weaponConfig = initialWeaponConfig;
            artifacts = new ArtifactsScriptableObject[initialWeaponConfig.slotsAmount];
        }
        
    }
}
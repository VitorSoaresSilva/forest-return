using Artifacts;
using Character;
using UnityEngine;

namespace Weapons
{
    public class  Weapon
    {
        public readonly WeaponsScriptableObject weaponConfig;
        public readonly ArtifactsScriptableObject[] artifacts;
        public Weapon(WeaponsScriptableObject initialWeaponConfig)
        {
            weaponConfig = initialWeaponConfig;
            artifacts = new ArtifactsScriptableObject[initialWeaponConfig.slotsAmount];
        }
        
    }
}
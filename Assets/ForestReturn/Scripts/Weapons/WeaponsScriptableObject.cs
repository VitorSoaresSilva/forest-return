using Artifacts;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "newWeapon", menuName = "ScriptableObject/Weapons")]
    public class WeaponsScriptableObject : ScriptableObject
    {
        public string weaponName;
        public int slotsAmount;
        public ArtifactsScriptableObject[] initialArtifacts;
    }
}
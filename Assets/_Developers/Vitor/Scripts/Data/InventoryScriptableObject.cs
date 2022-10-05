using Artifacts;
using UnityEngine;
using Weapons;

namespace ForestReturn.Scripts.Data
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class InventoryScriptableObject : ScriptableObject
    {
        public ArtifactsScriptableObject[] ArtifactsInventory;
        public WeaponsScriptableObject[] WeaponsInventory;
        public WeaponsScriptableObject WeaponEquiped;
        public ArtifactsScriptableObject[] ArtifactsEquiped;
    }
}
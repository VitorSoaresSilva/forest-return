using _Developers.Vitor.Scripts.Artifacts;
using _Developers.Vitor.Scripts.Weapons;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Data
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
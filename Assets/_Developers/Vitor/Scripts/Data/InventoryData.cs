using System;
using _Developers.Vitor.Scripts.Artifacts;

namespace _Developers.Vitor.Scripts.Data
{
    [Serializable]
    public class InventoryData
    {
        public float potato;
        // public ArtifactsScriptableObject[] ArtifactsInventory;
        // public WeaponsScriptableObject[] WeaponsInventory;
        // public WeaponsScriptableObject WeaponEquiped;
        public ArtifactsScriptableObject[] ArtifactsEquiped;
    }
}
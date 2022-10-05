using System;
using System.Collections.Generic;
using Artifacts;
using Weapons;

namespace ForestReturn.Scripts.Data
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
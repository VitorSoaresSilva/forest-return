using System;
using System.Collections.Generic;
using Artifacts;
using Weapons;

namespace ForestReturn.Scripts.Data
{
    [Serializable]
    public class InventoryData
    {
        public List<ArtifactsScriptableObject> ArtifactsInventory;
        public List<WeaponsScriptableObject> WeaponsInventory;
        public WeaponsScriptableObject WeaponEquiped;
        public ArtifactsScriptableObject[] ArtifactsEquiped;
    }
}
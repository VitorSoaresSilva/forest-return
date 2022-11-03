using ForestReturn.Scripts.Inventory;
using UnityEngine;

namespace ForestReturn.GameConfig
{
    public static class BlacksmithConfigData
    {
        public static WeaponCostByLevel[] LevelsCost { get; private set; } = new[]
        {
            new WeaponCostByLevel()
            {
                ScrapCost = 10,
                SeedCost = 20
            },new WeaponCostByLevel()
            {
                ScrapCost = 20,
                SeedCost = 40
            },new WeaponCostByLevel()
            {
                ScrapCost = 30,
                SeedCost = 60
            },
        };
    }

    public struct WeaponCostByLevel
    {
        public int SeedCost;
        public int ScrapCost;
        
    }

    public struct ItemTypeQuantity
    {
        public CurrencyType CurrencyType;
        public int Amount;
    }
}
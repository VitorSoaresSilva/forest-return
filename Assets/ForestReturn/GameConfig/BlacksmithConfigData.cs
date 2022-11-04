using ForestReturn.Scripts.Inventory;
using UnityEngine;

namespace ForestReturn.GameConfig
{
    public static class BlacksmithConfigData
    {
        public static CostByLevel[] LevelsCost { get; private set; } = new[]
        {
            new CostByLevel()
            {
                ScrapCost = 10,
                SeedCost = 20
            },new CostByLevel()
            {
                ScrapCost = 20,
                SeedCost = 40
            },new CostByLevel()
            {
                ScrapCost = 30,
                SeedCost = 60
            },
        };
        public static CostByLevel[] SlotsCost { get; private set; } = new[]
        {
            new CostByLevel()
            {
                ScrapCost = 10,
                SeedCost = 20
            },new CostByLevel()
            {
                ScrapCost = 20,
                SeedCost = 40
            },new CostByLevel()
            {
                ScrapCost = 30,
                SeedCost = 60
            },
        };
    }
    
    public struct CostByLevel
    {
        public int SeedCost;
        public int ScrapCost;
    }
}
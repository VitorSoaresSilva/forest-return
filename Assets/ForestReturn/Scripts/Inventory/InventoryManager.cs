using ForestReturn.Scripts.Artifacts;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        [HideInInspector] public InventoryObject inventory;
        // [HideInInspector] public InventoryObject equippedItems;
        [HideInInspector] public EquippedObject equippedItems;
        
        public ItemDatabaseObject Database;
        [Header("Triggers")] 
        public TriggerDatabaseObject triggerDatabase;
        [HideInInspector]
        public TriggerInventoryObject triggerInventory;
        
        public void Clear()
        {
            if (inventory != null)
            {
                inventory.Clear();
            }

            if (equippedItems != null)
            {
                equippedItems.Clear();
            }

            if (triggerInventory != null)
            {
                triggerInventory.Clear();
            }
        }
        public void Init() // New Game
        {
            inventory.Init();
            equippedItems.Init();
            triggerInventory.Init();
        }

        public void Load()
        {
            inventory.Load();
            equippedItems.Load();
            triggerInventory.Load();
        }

        [ContextMenu("Money")]
        public void DEBBUG_GetMoney()
        {
            
            var seed = inventory.FindCurrencyByType(CurrencyType.Seed);
            seed.AddAmount(50);
            var scrap = inventory.FindCurrencyByType(CurrencyType.Scrap);
            scrap.AddAmount(50);
        }
    }
}
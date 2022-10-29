using ForestReturn.Scripts.Artifacts;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        [HideInInspector] public InventoryObject inventory;
        [HideInInspector] public InventoryObject equippedItems;
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
        }
        public void Init() // New Game
        {
            inventory.Clear();
            equippedItems.Clear();
            triggerInventory.Init();
            // add initial sword to Equipped items
        }
        public void Init(InventoryObject loadedInventory,InventoryObject loadedEquippedItems)
        {
            inventory = loadedInventory;
            inventory.Init();
            equippedItems = loadedEquippedItems;
            equippedItems.Init();
        }


        
        // public void Save()
        // {
        //     inventory.path = GameManager.instance.InventorySavePath;
        //     equippedItems.path = GameManager.instance.EquippedSavePath;
        //     inventory.Save();
        //     equippedItems.Save();
        // }
        //  
        // public void Load()
        // {
        //     inventory.path = GameManager.instance.InventorySavePath;
        //     equippedItems.path = GameManager.instance.EquippedSavePath;
        //     inventory.Load();
        //     equippedItems.Load();
        // }

        public void TryEquipArtifact(ArtifactObject artifactObject) //bool
        {
            var slot = inventory.Find(artifactObject);
            if (slot != null)
            {
                
            }
        }
    }
}
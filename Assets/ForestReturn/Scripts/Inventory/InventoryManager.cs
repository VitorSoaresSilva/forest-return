using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.Artifacts;

namespace ForestReturn.Scripts.Inventory
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        public InventoryObject inventory;
        public InventoryObject equippedItems;
        public ItemDatabaseObject Database;

        public void Clear()
        {
            inventory.Clear();
            equippedItems.Clear();
        }
        public void Init() // New Game
        {
            inventory.Clear();
            equippedItems.Clear();
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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    public struct ItemCollectedData
    {
        public int CollectedAmount;
        public int CurrentAmount;
        public ItemObject Item;
    }
    [Serializable]
    [CreateAssetMenu(fileName = "new Inventory", menuName = "Items/Inventory", order = 0)]
    public class InventoryObject : ScriptableObject
    {
        [field: SerializeField] public List<InventorySlot> Items { get; private set; } = new();
        public delegate void OnItemCollectedEvent(ItemCollectedData itemCollectedData);
        public event OnItemCollectedEvent OnItemCollected;
        // public string path;
        public void AddItem(ItemObject item, int amount = 1)
        {
            if (item.isStackable)
            {
                foreach (var inventorySlot in Items)
                {
                    if (inventorySlot.item == item)
                    {
                        OnItemCollected?.Invoke(new ItemCollectedData{CollectedAmount = amount, CurrentAmount = inventorySlot.amount, Item = item});
                        inventorySlot.AddAmount(amount);
                        return;
                    }
                }
            }
            OnItemCollected?.Invoke(new ItemCollectedData{CollectedAmount = amount, CurrentAmount = amount,Item = item});
            Items.Add(new InventorySlot(item.id,amount,item));
        }

        public bool RemoveItem(ItemObject itemObject, int amount = 1)
        {
            var inventorySlot = Items.Find(inventorySlot => inventorySlot.item == itemObject);
            if (inventorySlot == null) return false;
            if (inventorySlot.amount < amount) return false;
            if (inventorySlot.amount > amount)
            {
                inventorySlot.amount -= amount;
                return true;
            }
            if (inventorySlot.amount != amount) return false;
            Items.Remove(inventorySlot);
            return true;
        }

        public void Load()
        {
            foreach (var inventorySlot in Items)
            {
                var b = InventoryManager.Instance.Database.items[inventorySlot.id];
                inventorySlot.item = b;
            }
        }

        public void Init() // new game
        {
            
        }

        // public bool SwitchSlots(InventorySlot oldObject, InventorySlot newObject)
        // {
        //     var oldRef = Items.Find(x=>x == oldObject);
        //     var newRef = Items.Find(x=>x == newObject);
        //     oldRef = newObject;
        //     newRef = oldObject;
        //     
        // }
        

        // [ContextMenu("Save")]
        // public void Save()
        // {
        //     string saveData = JsonUtility.ToJson(this, true);
        //     BinaryFormatter bf = new BinaryFormatter();
        //     FileStream file = File.Create(string.Concat(Application.persistentDataPath, path));
        //     bf.Serialize(file,saveData);
        //     file.Close();
        //     
        //     
        //
        //     /*
        //      // This code save the data in a binary file
        //     IFormatter formatter = new BinaryFormatter();
        //     Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,
        //         FileAccess.Write);
        //     formatter.Serialize(stream, Container);
        //     stream.Close();
        //     */
        // }

        // [ContextMenu("Load")]
        // public void Load()
        // {
        //     if (File.Exists(string.Concat(Application.persistentDataPath, path)))
        //     {
        //         BinaryFormatter bf = new BinaryFormatter();
        //         FileStream file = File.Open(string.Concat(Application.persistentDataPath, path), FileMode.Open);
        //         JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        //         file.Close();
        //         foreach (var inventorySlot in Items)
        //         {
        //             var b = InventoryManager.instance.Database.items[inventorySlot.id];
        //             inventorySlot.item = b;
        //         }
        //         
        //         
        //         /*
        //          // this code load the data from a binary file
        //         IFormatter formatter = new BinaryFormatter();
        //         Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,
        //             FileAccess.Read);
        //         Container = (Inventory)formatter.Deserialize(stream);
        //         stream.Close();
        //         */
        //     }
        // }

        [ContextMenu("Clear")]
        public void Clear()
        {
            // path = string.Empty;
            Items.Clear();
        }

        public List<InventorySlot> GetItemsByType(ItemType itemType)
        {
            var items = Items.FindAll(x => x.item.itemType == itemType);
            return items;
        }
        public List<InventorySlot> GetItemsByTypes(ItemType[] itemType)
        {
            var items = Items.FindAll(x => itemType.Contains(x.item.itemType));
            return items;
        }
        
        public List<InventorySlot> GetPotionByType(PotionType potionType)
        {
            var items = Items.FindAll(x =>
                x.item.itemType == ItemType.Potion && ((PotionObject)x.item).potionType == potionType);
            return items;
        }

        public InventorySlot Find(ItemObject itemObject)
        {
            var item = Items.Find(x => x.item == itemObject);
            return item;
        }

        public InventorySlot FindCurrencyByType(CurrencyType currencyType)
        {
            var currency = GetItemsByType(ItemType.Currency)
                .Find(x =>
                {
                    var a = (CurrencyObject)x.item;
                    return a.type == currencyType;
                });
            return currency;
            
        }
    }
}
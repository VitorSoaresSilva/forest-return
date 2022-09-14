using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "new Inventory", menuName = "Items/Inventory", order = 0)]
    public class InventoryObject : ScriptableObject
    {
        [field: SerializeField] public List<InventorySlot> Items { get; private set; } = new();
        public void AddItem(ItemObject item, int amount = 1)
        {
            if (item.isStackable)
            {
                foreach (var inventorySlot in Items)
                {
                    if (inventorySlot.item == item)
                    {
                        inventorySlot.AddAmount(amount);
                        return;
                    }
                }
            }
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

        [ContextMenu("Save")]
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath,InventoryManager.instance.savePath));
            bf.Serialize(file,saveData);
            file.Close();
            Debug.Log(InventoryManager.instance.Database);
            
            

            /*
             // This code save the data in a binary file
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,
                FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();
            */
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath,InventoryManager.instance.savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, InventoryManager.instance.savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
                foreach (var inventorySlot in Items)
                {
                    var b = InventoryManager.instance.Database.GetItem[inventorySlot.id];
                    inventorySlot.item = b;
                }
                
                
                /*
                 // this code load the data from a binary file
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,
                    FileAccess.Read);
                Container = (Inventory)formatter.Deserialize(stream);
                stream.Close();
                */
                
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Items.Clear();
        }

        public List<InventorySlot> GetItemsByType(ItemType itemType)
        {
            var items = Items.FindAll(x => x.item.itemType == itemType);
            return items;
        }

        public List<InventorySlot> GetPotionByType(PotionType potionType)
        {
            var items = Items.FindAll(x =>
                x.item.itemType == ItemType.Potion && ((PotionObject)x.item).potionType == potionType);
            return items;
        }
    }
}
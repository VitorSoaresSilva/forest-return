using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "new Inventory", menuName = "Items/Inventory", order = 0)]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public ItemDatabaseObject database;
        public Inventory Container;
        public void AddItem(Item item, int amount = 1)
        {
            // if (item.isStackable)
            // {
                foreach (var inventorySlot in Container.Items)
                {
                    if (inventorySlot.item == item)
                    {
                        inventorySlot.AddAmount(amount);
                        return;
                    }
                }
            // }
            Container.Items.Add(new InventorySlot(item.id,amount,item));
        }

        [ContextMenu("Save")]
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath,savePath));
            bf.Serialize(file,saveData);
            file.Close();

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
            if (File.Exists(string.Concat(Application.persistentDataPath,savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
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
            Container = new Inventory();
        }
    }
}
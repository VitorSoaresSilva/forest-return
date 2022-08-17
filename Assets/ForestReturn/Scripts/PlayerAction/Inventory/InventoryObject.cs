using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "new Inventory", menuName = "Items/Inventory", order = 0)]
    public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public string savePath;
        private ItemDatabaseObject database;
        public List<InventorySlot> Container = new ();

        private void OnEnable()
        {
            #if UNITY_EDITOR
                database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/_Developers/Vitor/Resources/Database.asset",typeof(ItemDatabaseObject));
            #else
                database = Resources.Load<ItemDatabaseObject>("Database");
            #endif
        }

        public void AddItem(ItemObject item, int amount = 1)
        {
            if (item.isStackable)
            {
                foreach (var inventorySlot in Container)
                {
                    if (inventorySlot.item == item)
                    {
                        inventorySlot.AddAmount(amount);
                        return;
                    }
                }
            }
            Container.Add(new InventorySlot(database.GetId[item],amount,item));
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            foreach (var t in Container)
            {
                t.item = database.GetItem[t.id];
            }
        }

        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath,savePath));
            bf.Serialize(file,saveData);
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath,savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
            }
        }
    }
}
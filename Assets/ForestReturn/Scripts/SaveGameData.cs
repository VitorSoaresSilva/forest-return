using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts
{
    [CreateAssetMenu(fileName = "newSaveGameData", menuName = "SaveGameData")]
    public class SaveGameData: ScriptableObject
    {
        public string path = "";
        public bool loadSuccess;
        public InventoryObject inventoryObject;
        public InventoryObject equippedObject;
        public GeneralDataObject generalDataObject;
        public TriggerInventoryObject triggerInventoryObject;
        public void Save()
        {
            var dataSerialized = new DataSerialized()
            {
                InventoryObjectJson = JsonUtility.ToJson(inventoryObject, true),
                EquippedObjectJson = JsonUtility.ToJson(equippedObject, true),
                GeneralDataObjectJson = JsonUtility.ToJson(generalDataObject, true),
                TriggerInventoryObjectJson = JsonUtility.ToJson(triggerInventoryObject, true),
            };
            string saveData = JsonUtility.ToJson(dataSerialized,true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, path));
            bf.Serialize(file,saveData);
            file.Close();
        }

        public void Load(string path)
        {
            this.path = path;
            if (File.Exists(string.Concat(Application.persistentDataPath, this.path)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, this.path), FileMode.Open);
                DataSerialized a = new DataSerialized();
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), a);
                file.Close();
                FromJson(a);
                loadSuccess = true;
                return;
            }
            loadSuccess = false;
        }

        public void Delete(string path)
        {
            this.path = path;
            if (File.Exists(string.Concat(Application.persistentDataPath, this.path)))
            {
                File.Delete(string.Concat(Application.persistentDataPath, this.path));
                Clear();
            }
        }

        public void Clear()
        {
            Debug.Log("Clear");
            inventoryObject.Clear();
            equippedObject.Clear();
            generalDataObject.Clear();
            triggerInventoryObject.Clear();
            loadSuccess = false;
        }

        public void FromJson(DataSerialized dataSerialized)
        {
            JsonUtility.FromJsonOverwrite(dataSerialized.GeneralDataObjectJson, generalDataObject);
            JsonUtility.FromJsonOverwrite(dataSerialized.InventoryObjectJson, inventoryObject);
            JsonUtility.FromJsonOverwrite(dataSerialized.EquippedObjectJson, equippedObject);
            JsonUtility.FromJsonOverwrite(dataSerialized.TriggerInventoryObjectJson, triggerInventoryObject);
        }
    }

    
    public class DataSerialized
    {
        public string InventoryObjectJson;
        public string EquippedObjectJson;
        public string GeneralDataObjectJson;
        public string TriggerInventoryObjectJson;
    }
}
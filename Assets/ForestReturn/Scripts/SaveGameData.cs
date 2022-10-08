using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts
{
    [CreateAssetMenu(fileName = "newSaveGameData", menuName = "SaveGameData")]
    public class SaveGameData: ScriptableObject
    {
        public string Path = "";
        public bool LoadSuccess = false;
        
        public InventoryObject inventoryObject;
        public InventoryObject equippedObject;
        public GeneralDataObject generalDataObject;
        public TriggerInventoryObject triggerInventoryObject;
        public string inventoryObjectJson;
        public string equippedObjectJson;
        public string generalDataObjectJson;
        public string triggerInventoryObjectJson;
        
        
        
        public void Save()
        {
            ToJson();
            string saveData = JsonUtility.ToJson(this,true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, Path));
            bf.Serialize(file,saveData);
            file.Close();
            
            
        }

        public void Load(string path)
        {
            Path = path;
            if (File.Exists(string.Concat(Application.persistentDataPath, Path)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, Path), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
                FromJson();
                LoadSuccess = true;
                return;
            }
            LoadSuccess = false;
        }

        public void Init()
        {
            inventoryObject = CreateInstance<InventoryObject>();
            equippedObject = CreateInstance<InventoryObject>();
            generalDataObject = CreateInstance<GeneralDataObject>();
            triggerInventoryObject = CreateInstance<TriggerInventoryObject>();
        }

        public void Clear()
        {
            inventoryObjectJson = "";
            equippedObjectJson = "";
            generalDataObjectJson = "";
            triggerInventoryObjectJson = "";
            
            inventoryObject.Clear();
            equippedObject.Clear();
            generalDataObject.Clear();
            triggerInventoryObject.Clear();
            LoadSuccess = false;
        }

        public void ToJson()
        {
            generalDataObjectJson = JsonUtility.ToJson(generalDataObject);
            inventoryObjectJson = JsonUtility.ToJson(inventoryObject);
            equippedObjectJson = JsonUtility.ToJson(equippedObject);
            triggerInventoryObjectJson = JsonUtility.ToJson(triggerInventoryObject);
        }

        public void FromJson()
        {
            JsonUtility.FromJsonOverwrite(generalDataObjectJson, generalDataObject);
            JsonUtility.FromJsonOverwrite(inventoryObjectJson, inventoryObject);
            JsonUtility.FromJsonOverwrite(equippedObjectJson, equippedObject);
            JsonUtility.FromJsonOverwrite(triggerInventoryObjectJson, triggerInventoryObject);
        }
    }
    
    
}
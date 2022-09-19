using System;
using Utilities;
using System.IO;
using ForestReturn.Scripts.PlayerAction.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public readonly float[] PercentageIncreaseByLevelWeapon = new []{1f,1.1f,1.2f};
        public GameDataObject gameDataObject;
        private string _indexSaveSlot = "0";
        public string InventorySavePath => $"/gameData_{_indexSaveSlot}_inventory.data";
        public string EquippedSavePath => $"/gameData_{_indexSaveSlot}_equipped.data";
        public string GameDataSavePath => $"/gameData_{_indexSaveSlot}_gameData.data";
        public string TriggersSavePath => $"/gameData_{_indexSaveSlot}_triggers.data";

        [Header("Triggers")] 
        [HideInInspector] public TriggerDatabaseObject triggerDatabase;
        [FormerlySerializedAs("triggerInventoryObject")] public TriggerInventoryObject triggerInventory;

        [ContextMenu("Play")]
        public void Play()
        {
            SceneManager.LoadScene((int)gameDataObject.currentLevel);
            LoadGame();
        }
        [ContextMenu("Save")]
        public void Save()
        {
            // gameDataObject.path = GameDataSavePath;
            gameDataObject.LastSave = new DateTime();
            gameDataObject.Save();
            
            // triggerInventory.path = TriggersSavePath;
            triggerInventory.Save();
            
            InventoryManager.instance.Save();
            //save skills
        }
        public void LoadGame()
        {
            gameDataObject.path = GameDataSavePath;
            gameDataObject.Load();
            
            triggerInventory.path = TriggersSavePath;
            triggerInventory.Load();
            
            //load skills
        }

        public void SelectIndexSaveSlot(int index)
        {
            _indexSaveSlot = index.ToString();
            gameDataObject.path = GameDataSavePath;
            triggerInventory.path = TriggersSavePath;
            InventoryManager.instance.inventory.path = InventorySavePath;
            InventoryManager.instance.equippedItems.path = EquippedSavePath;
        }

        public bool[] GetAvailableSaves()
        {
            bool[] data = new bool[3];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = File.Exists(string.Concat(Application.persistentDataPath, $"/gameData_{i}_gameData.data"));
            }
            return data;
        }

        private void OnApplicationQuit()
        {
            triggerInventory.Clear();
        }
    }
}
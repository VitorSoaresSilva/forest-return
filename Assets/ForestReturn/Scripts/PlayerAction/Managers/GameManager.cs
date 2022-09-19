using System;
using Utilities;
using System.IO;
using ForestReturn.Scripts.PlayerAction.Teleport;
using ForestReturn.Scripts.PlayerAction.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums = ForestReturn.Scripts.PlayerAction.Utilities.Enums;


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
        public TriggerDatabaseObject triggerDatabase;
        public TriggerInventoryObject triggerInventory;
        
        // [Header("Game State")]
        // [field: SerializeField] public Enums.Scenes currentScene { get; private set; }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [ContextMenu("Play")]
        public void Play()
        {
            Debug.Log("load");
            LoadGame();
            SceneManager.LoadScene((int)gameDataObject.currentLevel);
        }
        [ContextMenu("Save")]
        public void Save()
        {
            gameDataObject.path = GameDataSavePath;
            gameDataObject.LastSave = new DateTime();
            gameDataObject.Save();
            
            triggerInventory.path = TriggersSavePath;
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

        public void HandleTeleport(TeleportData? teleportData)
        {
            // estou em um level -> envio "level x"
            // estou no lobby => envio vazio

            if (teleportData != null)
            {
                gameDataObject.TeleportData = teleportData.Value;
                gameDataObject.currentLevel = Enums.Scenes.Lobby;
                //delay
                SceneManager.LoadSceneAsync((int)Enums.Scenes.Lobby, LoadSceneMode.Single);
                return;
            }

            if (gameDataObject.TeleportData.AlreadyReturned) return;
            gameDataObject.currentLevel = gameDataObject.TeleportData.SceneStartIndex;
            //delay
            SceneManager.LoadSceneAsync((int)gameDataObject.TeleportData.SceneStartIndex, LoadSceneMode.Single);

        }

        private void OnApplicationQuit()
        {
            triggerInventory.Clear();
            gameDataObject.Clear();
        }
    }
}
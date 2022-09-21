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
        public GameDataObject gameDataObject;
        private string _indexSaveSlot = "0";
        public string InventorySavePath => $"/gameData_{_indexSaveSlot}_inventory.data";
        public string EquippedSavePath => $"/gameData_{_indexSaveSlot}_equipped.data";
        private string GameDataSavePath => $"/gameData_{_indexSaveSlot}_gameData.data";
        private string TriggersSavePath => $"/gameData_{_indexSaveSlot}_triggers.data";

        [Header("Triggers")] 
        public TriggerDatabaseObject triggerDatabase;
        public TriggerInventoryObject triggerInventory;
        public TriggerObject hammerFromBlacksmith;
        
        public readonly float[] PercentageIncreaseByLevelWeapon = new []{1f,1.1f,1.2f};
        public int MaxArtifacts { get; private set; } = 2;
        
        [ContextMenu("Play")]
        public void Play()
        {
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

            InventoryManager.instance.Load();
            //load skills

            Init();
        }

        private void Init()
        {
            if (triggerInventory.Contains(hammerFromBlacksmith))
            {
                MaxArtifacts = 3;
            }
            //...
        }

        public void SelectIndexSaveSlot(int index)
        {
            _indexSaveSlot = index.ToString();
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
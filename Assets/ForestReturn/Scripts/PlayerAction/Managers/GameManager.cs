using System;
using System.Globalization;
using Utilities;
using System.IO;
using ForestReturn.Scripts.PlayerAction.Inventory;
using ForestReturn.Scripts.PlayerAction.Teleport;
using ForestReturn.Scripts.PlayerAction.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Enums = ForestReturn.Scripts.PlayerAction.Utilities.Enums;


namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [FormerlySerializedAs("gameDataObject")] public GeneralDataObject generalData;
        public int IndexSaveSlot { get; private set; } = -1;
        // private int _indexLatestSaveSlot = -1;

        // public GameData GameData;
        public SaveGameData[] savedGameDataTemporary;

        [Header("Triggers")] 
        public TriggerDatabaseObject triggerDatabase;
        public TriggerInventoryObject triggerInventory;
        public TriggerObject hammerFromBlacksmith;
        public readonly float[] PercentageIncreaseByLevelWeapon = new []{1f,1.1f,1.2f};
        public int MaxArtifacts { get; private set; } = 2;

        private void Start()
        {
            
            // SavedGameDataTemporary = new SaveGameData[3];
            for (int i = 0; i < 3; i++)
            {
                // SavedGameDataTemporary[i] = ScriptableObject.CreateInstance<SaveGameData>();
                // savedGameDataTemporary[i].Init();
                savedGameDataTemporary[i].Load($"/gameData_{i}.data");
                if (savedGameDataTemporary[i].LoadSuccess)
                {
                    if (IndexSaveSlot == -1 || savedGameDataTemporary[i].generalDataObject.LastSaveLong >
                        savedGameDataTemporary[IndexSaveSlot].generalDataObject.LastSaveLong)
                    {
                        IndexSaveSlot = i;
                    }
                }
            }

            if (IndexSaveSlot != -1)
            {
                //continue button enable
            }
        }
        public void SelectIndexSaveSlot(int index)
        {
            IndexSaveSlot = index;
        }

        [ContextMenu("Play")]
        public void Play()
        {
            if (IndexSaveSlot == -1) return;
            Init();
            SceneManager.LoadScene((int)generalData.currentLevel);
        }

        [ContextMenu("Save")]
        public void Save()
        {

            generalData.LastSaveString = DateTime.Today.ToLongTimeString();
            generalData.LastSaveLong = DateTime.Now.ToFileTime();
            savedGameDataTemporary[IndexSaveSlot].Save();
            //save skills
        }


        private void Init()
        {


            if (IndexSaveSlot == -1) return;
            InventoryManager.instance.inventory = savedGameDataTemporary[IndexSaveSlot].inventoryObject;
            InventoryManager.instance.equippedItems = savedGameDataTemporary[IndexSaveSlot].equippedObject;
            triggerInventory = savedGameDataTemporary[IndexSaveSlot].triggerInventoryObject;
            generalData = savedGameDataTemporary[IndexSaveSlot].generalDataObject;


            if (savedGameDataTemporary[IndexSaveSlot].LoadSuccess)
            {
                
            }
            else
            {
                InventoryManager.instance.Init();
                triggerInventory.Init();
                generalData.Init();
            }

            // if (triggerIn ventory.Contains(hammerFromBlacksmith))
            // {
            //     MaxArtifacts = 3;
            // }
        }

        public void HandleTeleport(TeleportData? teleportData)
        {
            if (teleportData != null)
            {
                generalData.TeleportData = teleportData.Value;
                generalData.currentLevel = Enums.Scenes.Lobby;
                //delay
                SceneManager.LoadSceneAsync((int)Enums.Scenes.Lobby, LoadSceneMode.Single);
                return;
            }

            if (generalData.TeleportData.AlreadyReturned) return;
            generalData.currentLevel = generalData.TeleportData.SceneStartIndex;
            //delay
            SceneManager.LoadSceneAsync((int)generalData.TeleportData.SceneStartIndex, LoadSceneMode.Single);

        }

        private void OnApplicationQuit()
        {
            for (int i = 0; i < savedGameDataTemporary.Length; i++)
            {
                savedGameDataTemporary[i].Clear();
            }
        }
    }
}
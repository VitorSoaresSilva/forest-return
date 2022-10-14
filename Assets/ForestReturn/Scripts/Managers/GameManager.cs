using System;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Enums = ForestReturn.Scripts.Utilities.Enums;


namespace ForestReturn.Scripts.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [HideInInspector] public GeneralDataObject generalData;
        public int IndexSaveSlot { get; private set; } = -1;
        // private int _indexLatestSaveSlot = -1;

        // public GameData GameData;
        public SaveGameData[] savedGameDataTemporary;

        [Header("Triggers")] 
        public TriggerDatabaseObject triggerDatabase;
        [HideInInspector]
        public TriggerInventoryObject triggerInventory;
        public TriggerObject hammerFromBlacksmith;
        public readonly float[] PercentageIncreaseByLevelWeapon = new []{1f,1.1f,1.2f};
        public int MaxArtifacts { get; private set; } = 2;

        public Button continueBtn;

        private void Start()
        {
            
            // SavedGameDataTemporary = new SaveGameData[3];
            for (int i = 0; i < 3; i++)
            {
                savedGameDataTemporary[i].Load($"/gameData_{i}.data");
                if (savedGameDataTemporary[i].loadSuccess)
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
                continueBtn.gameObject.SetActive(true);
                continueBtn.enabled = true;
                //continue button enable
            }
            else
            {
                continueBtn.gameObject.SetActive(false);
                continueBtn.enabled = false;
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
            generalData.LastSaveString = DateTime.Now.ToLongTimeString();
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


            if (savedGameDataTemporary[IndexSaveSlot].loadSuccess)
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

        public void ChangeScene(Enums.Scenes scene)
        {
            generalData.currentLevel = scene;
            //TODO: Add Effect teleport
            generalData.TeleportData = new TeleportData();
            SceneManager.LoadScene((int)scene);
        }
    }
}
using System;
using System.Collections;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.UI;
using TMPro;
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
        public bool isPaused { get; private set; }
        // private int _indexLatestSaveSlot = -1;

        // public GameData GameData;
        public SaveGameData[] savedGameDataTemporary;

        [Header("Triggers")] 
        public TriggerDatabaseObject triggerDatabase;
        [HideInInspector]
        public TriggerInventoryObject triggerInventory;
        public bool loadingFromCheckpoint;
        public delegate void OnGameManagerInitFinishedEvent();
        public event OnGameManagerInitFinishedEvent OnGameManagerInitFinished;
        public bool GameManagerInitFinished { get; private set; } = false;

        private void Start()
        {
            LoadDataFromFiles();
        }

        private void LoadDataFromFiles()
        {
            GameManagerInitFinished = false;
            loadingFromCheckpoint = false;
            IndexSaveSlot = -1;
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
            GameManagerInitFinished = true;
            OnGameManagerInitFinished?.Invoke();
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
            generalData.playerPosition = LevelManager.instance.PlayerScript.transform.position;
            savedGameDataTemporary[IndexSaveSlot].Save();
            //save skills
        }


        private void Init()
        {
            if (IndexSaveSlot == -1) return;
            InventoryManager.instance.inventory = null;
            InventoryManager.instance.equippedItems = null;
            // InventoryManager.instance.Clear();
            if (triggerInventory != null)
            {
                // triggerInventory.Clear();
                triggerInventory = null;
            }
            if (generalData != null)
            {
                generalData = null;
                // generalData.Clear();
            }
            InventoryManager.instance.inventory = savedGameDataTemporary[IndexSaveSlot].inventoryObject;
            InventoryManager.instance.equippedItems = savedGameDataTemporary[IndexSaveSlot].equippedObject;
            triggerInventory = savedGameDataTemporary[IndexSaveSlot].triggerInventoryObject;
            generalData = savedGameDataTemporary[IndexSaveSlot].generalDataObject;


            if (savedGameDataTemporary[IndexSaveSlot].loadSuccess)
            {
                loadingFromCheckpoint = true;
            }
            else
            {
                InventoryManager.instance.Init();
                
                triggerInventory.Init();
                generalData.Init();
            }
        }

        public void HandleTeleport(TeleportData? teleportData)
        {
            if (teleportData != null)
            {
                generalData.TeleportData = teleportData.Value;
                generalData.currentLevel = Enums.Scenes.Lobby;
                StartCoroutine(LoadScene((int)Enums.Scenes.Lobby));
                return;
            }

            if (generalData.TeleportData.AlreadyReturned) return;
            generalData.currentLevel = generalData.TeleportData.SceneStartIndex;
            StartCoroutine(LoadScene((int)generalData.TeleportData.SceneStartIndex));
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
            yield return null;
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
        
        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
            LevelManager.instance.OnResumeGame();
            UiManager.instance.OpenCanvas(CanvasType.Hud);
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
            LevelManager.instance.OnPauseGame();
        }

        public void BackToMainMenu()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            SceneManager.LoadScene((int)Enums.Scenes.MainMenu); 
            LoadDataFromFiles();
        }
        public void ExitGame()
        {
            //TODO:Salvar jogo
            Application.Quit();
        }

        public void RestartFromCheckpoint()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            LoadDataFromFiles();
            Play();
        }

        public void DeleteSlotIndex()
        {
            savedGameDataTemporary[IndexSaveSlot].Delete($"/gameData_{IndexSaveSlot}.data");
        }
    }
    
}
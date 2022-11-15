using System;
using System.Collections;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums = ForestReturn.Scripts.Utilities.Enums;


namespace ForestReturn.Scripts.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [HideInInspector] public GeneralDataObject generalData;
        public int IndexSaveSlot { get; private set; } = -1;
        public bool IsPaused { get; private set; }
        public SaveGameData[] savedGameDataTemporary;
        public bool loadingFromCheckpoint;
        public delegate void OnGameManagerInitFinishedEvent();
        public delegate void OnResumeGameEvent();
        public delegate void OnPauseGameEvent();
        public event OnGameManagerInitFinishedEvent OnGameManagerInitFinished;
        public event OnResumeGameEvent OnResumeGame;
        public event OnPauseGameEvent OnPauseGame;
        
        
        public bool GameManagerInitFinished { get; private set; } = false;

        private void Start()
        {
            LoadDataFromFiles();
        }

        private void LoadDataFromFiles()
        {
            Clear();
            GameManagerInitFinished = false;
            loadingFromCheckpoint = false;
            IndexSaveSlot = -1;
            for (int i = 0; i < 3; i++)
            {
                savedGameDataTemporary[i].Load($"/gameData_{i}.data");
                if (savedGameDataTemporary[i].loadSuccess)
                {
                    if (IndexSaveSlot == -1 || savedGameDataTemporary[i].generalDataObject.lastSaveLong >
                        savedGameDataTemporary[IndexSaveSlot].generalDataObject.lastSaveLong)
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
        }

        [ContextMenu("Save")]
        public void Save()
        {
            generalData.lastSaveString = DateTime.Now.ToLongTimeString();
            generalData.lastSaveLong = DateTime.Now.ToFileTime();
            generalData.playerPosition = LevelManager.Instance.PlayerScript.transform.position;
            generalData.currentLevel = LevelManager.Instance.sceneIndex;
            generalData.SetPlayerData();
            savedGameDataTemporary[IndexSaveSlot].Save();
            //save skills
        }

        public void Clear()
        {
            InventoryManager.Instance.Clear();
            if (generalData != null)
            {
                generalData.Clear();
            }
        }
        private void Init()
        {
            if (IndexSaveSlot == -1) return;
            InventoryManager.Instance.inventory = savedGameDataTemporary[IndexSaveSlot].inventoryObject;
            InventoryManager.Instance.equippedItems = savedGameDataTemporary[IndexSaveSlot].equippedObject;
            InventoryManager.Instance.triggerInventory = savedGameDataTemporary[IndexSaveSlot].triggerInventoryObject;
            generalData = savedGameDataTemporary[IndexSaveSlot].generalDataObject;
            if (savedGameDataTemporary[IndexSaveSlot].loadSuccess)
            {
                loadingFromCheckpoint = true;
                InventoryManager.Instance.Load();
            }
            else
            {
                InventoryManager.Instance.Init();
                generalData.Init();
            }
            SceneManager.LoadScene((int)generalData.currentLevel);
        }

        public void HandleTeleport(TeleportData? newTeleportData)
        {
            generalData.SetPlayerData();
            if (newTeleportData == null)
            {
                StartCoroutine(LoadScene((int)generalData.TeleportScene));
                // ChangeScene(generalData.TeleportScene);
            }
            else
            {
                generalData.SetTeleportData((TeleportData)newTeleportData);
                StartCoroutine(LoadScene((int)Enums.Scenes.Lobby));
                // ChangeScene(Enums.Scenes.Lobby);
            }
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            yield return new WaitForSeconds(1f);
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
            //TODO: Add Effect teleport
            Save();
            SceneManager.LoadScene((int)scene);
        }
        
        public void ResumeGame()
        {
            IsPaused = false;
            Time.timeScale = 1;
            OnResumeGame?.Invoke();
            // LevelManager.Instance.OnResumeGame();
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }

        public void PauseGame()
        {
            IsPaused = true;
            Time.timeScale = 0;
            OnPauseGame?.Invoke();
            // LevelManager.Instance.OnPauseGame();
        }

        public void BackToMainMenu()
        {
            if (IsPaused)
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
            if (IsPaused)
            {
                ResumeGame();
            }
            LoadDataFromFiles();
            Play();
        }

        public void DeleteSlotIndex()
        {
            savedGameDataTemporary[IndexSaveSlot].Delete($"/gameData_{IndexSaveSlot}.data");
            // Save();
            LoadDataFromFiles();
        }
    }
    
}
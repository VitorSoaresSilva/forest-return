using System;
using System.Collections;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.UI;
using ForestReturn.Scripts.Utilities;
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
        public SaveGameData[] savedGameDataTemporary;
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
            Clear();
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
            
        }

        [ContextMenu("Save")]
        public void Save()
        {
            generalData.LastSaveString = DateTime.Now.ToLongTimeString();
            generalData.LastSaveLong = DateTime.Now.ToFileTime();
            generalData.playerPosition = LevelManager.Instance.PlayerScript.transform.position;
            generalData.currentLevel = LevelManager.Instance.sceneIndex;
            generalData.playerCharacterData = new BaseCharacterData()
            {
                CurrentHealth = LevelManager.Instance.PlayerScript.CurrentHealth,
                CurrentMana = LevelManager.Instance.PlayerScript.CurrentMana
            };
            savedGameDataTemporary[IndexSaveSlot].Save();
            //save skills
        }

        public void Clear()
        {
            InventoryManager.Instance.Clear();
            if (generalData != null)
            {
                Debug.Log("Clear 2");
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
            Debug.Log("equals null on init " + generalData.TeleportData == null);

            if (savedGameDataTemporary[IndexSaveSlot].loadSuccess)
            {
                loadingFromCheckpoint = true;
            }
            else
            {
                Debug.Log("init");
                InventoryManager.Instance.Init();
                
                // InventoryManager.Instance.triggerInventory.Init();
                generalData.Init();
            }
            SceneManager.LoadScene((int)generalData.currentLevel);
        }

        public void HandleTeleport(TeleportData? teleportData)
        {
            if (teleportData != null)
            {
                generalData.TeleportData = teleportData.Value;
                // generalData.currentLevel = Enums.Scenes.Lobby;
                // StartCoroutine(LoadScene((int)Enums.Scenes.Lobby));
                ChangeScene(Enums.Scenes.Lobby);
            }
            else
            {
                ChangeScene(generalData.TeleportData.Value.SceneStartIndex);
                generalData.TeleportData = null;
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
            isPaused = false;
            Time.timeScale = 1;
            LevelManager.Instance.OnResumeGame();
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
            LevelManager.Instance.OnPauseGame();
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
            // Save();
            LoadDataFromFiles();
        }
    }
    
}
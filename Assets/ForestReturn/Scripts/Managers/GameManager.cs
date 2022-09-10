using System;
using Artifacts;
using ForestReturn.Scripts.Data;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.NaoSei;
using Player;
using UI;
using UnityEngine;
using Utilities;
using Weapons;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public static event Action<GameState> OnGameStateChanged;
        public GameState GameState { get; private set; }
        [field: SerializeField] public GameObject playerPrefab { get; private set; }
        [field: SerializeField] public GameObject cameraPrefab { get; private set; }
        public InventoryScriptableObject InventoryScriptableObject;
        [SerializeField] private WeaponsScriptableObject initialWeapon;
        [SerializeField] private ArtifactsScriptableObject[] initialArtifacts;
        public ConfigLobby configLobby;
        public PlayerMain PlayerMain { get; set; }

        public void ChangeGameState(GameState newGameState)
        {
            if (newGameState != GameState )
            {
                GameState = newGameState;
                OnGameStateChanged?.Invoke(newGameState);
            }
        }
        private void HandleExitMainMenu()
        {
            // UiManager.instance.HideMainMenu();
        }

        public void LoadScene(Enums.Scenes newScene)
        {
            MySceneLoader.instance.sceneLoaded += HandleSceneLoaded;
            MySceneLoader.instance.LoadScenes(new []{(int)newScene});
        }

        private void HandleSceneLoaded()
        {
            GameState newGameState = LevelManager.instance.State;
            UiManager.instance.HideAllPanel();
            ChangeGameState(LevelManager.instance.State);
            switch (newGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Lobby:
                case GameState.LobbySemCutscene:
                    HandleLobby();
                    break;
                case GameState.Pause:
                    break;
                case GameState.Level01:
                    // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Enums.Scenes.Level01));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleLobby()
        {
        }

        private void HandleMainMenu()
        {
            // UiManager.instance.HideMainMenu();
        }

        private void Start()
        {
            if (InventoryScriptableObject.WeaponEquiped == null && InventoryScriptableObject.WeaponsInventory.Length == 0){
                InventoryScriptableObject.WeaponEquiped = initialWeapon;
                InventoryScriptableObject.ArtifactsEquiped = initialArtifacts;
                InventoryScriptableObject.ArtifactsInventory = Array.Empty<ArtifactsScriptableObject>();
                InventoryScriptableObject.WeaponsInventory = Array.Empty<WeaponsScriptableObject>();
            }
            ChangeGameState(GameState.MainMenu);
        }
    }

    public enum GameState
    {
        None,
        MainMenu,
        Lobby,
        Pause,
        Level01,
        LobbySemCutscene
    }
}
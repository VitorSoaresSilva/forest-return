using System;
using Player;
using UI;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public static event Action<GameState> OnGameStateChanged;
        public GameState GameState { get; private set; }
        [field: SerializeField] public GameObject playerPrefab { get; private set; }
        private PlayerMain _playerMain;
        public PlayerMain PlayerMain
        {
            get
            {
                if (_playerMain == null)
                {
                    // TODO: Botar player no topo do bagulho
                    _playerMain = FindObjectOfType<PlayerMain>();
                }
                return _playerMain;
            }
        }
    
        public void ChangeGameState(GameState newGameState)
        {
            if (newGameState != GameState )
            {
                switch (GameState)
                {
                    case GameState.MainMenu:
                        HandleExitMainMenu();
                        break;
                    case GameState.Lobby:
                        break;
                    case GameState.Pause:
                        break;
                    case GameState.Level01:
                        break;
                    case GameState.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                GameState = newGameState;
                OnGameStateChanged?.Invoke(newGameState);
            }
        }

        // private void HandleLobby()
        // {
        //     LevelManager.instance.SpawnPlayer();
        // }

        private void HandleExitMainMenu()
        {
            UiManager.instance.HideMainMenu();
        }

        public void LoadScene(Enums.Scenes newScene)
        {
            // TODO: Dar unload nas outras cenas caso esteja vindo de dentro do jogo
            // MySceneLoader.instance.UnloadAnotherScenes(new []{(int)newScene,(int)Enums.Scenes.MainMenu});
            // MySceneLoader.instance.UnloadAnotherScenes(new []{(int) Enums.Scenes.MainMenu, (int)newScene});
            MySceneLoader.instance.sceneLoaded += HandleSceneLoaded;
            MySceneLoader.instance.LoadScenes(new []{(int)newScene});
        }

        private void HandleSceneLoaded()
        {
            // MySceneLoader.instance.sceneLoaded -= HandleSceneLoaded;
            // Debug.Log("handle scene loaded " + LevelManager.instance.State);
            GameState newGameState = LevelManager.instance.State;
            ChangeGameState(LevelManager.instance.State);
            switch (newGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Lobby:
                    // HandleLobby();
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

        private void HandleMainMenu()
        {
            // UiManager.instance.HideMainMenu();
        }

        private void Start()
        {
            ChangeGameState(GameState.MainMenu);
        }
    }

    public enum GameState
    {
        None,
        MainMenu,
        Lobby,
        Pause,
        Level01
    }
}
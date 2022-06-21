using System;
using Player;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<GameState> OnGameStateChanged;
        public GameState GameState { get; private set; }
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
            // Handle Exit state
            if (newGameState != GameState)
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                GameState = newGameState;
                OnGameStateChanged?.Invoke(newGameState);
            }
            switch (newGameState)
            {
                case GameState.MainMenu:
                    HandleMainMenu();
                    break;
                case GameState.Lobby:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Level01:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }
        }

        private void HandleExitMainMenu()
        {
            UiManager.instance.HideMainMenu();
        }

        public void LoadScene(Enums.Scenes newScene)
        {
            // TODO: Dar unload nas outras cenas caso esteja vindo de dentro do jogo
            MySceneLoader.instance.UnloadAnotherScenes(new []{(int)newScene,(int)Enums.Scenes.MainMenu});
            MySceneLoader.instance.LoadScenes(new []{(int)newScene});
            MySceneLoader.instance.sceneLoaded += HandleSceneLoaded;
        }

        private void HandleSceneLoaded()
        {
            MySceneLoader.instance.sceneLoaded -= HandleSceneLoaded;
            GameState newGameState = LevelManager.instance.State;
            ChangeGameState(newGameState);
            switch (newGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Lobby:
                    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Enums.Scenes.Lobby));
                    break;
                case GameState.Pause:
                    break;
                case GameState.Level01:
                    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Enums.Scenes.Level01));
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
        MainMenu,
        Lobby,
        Pause,
        Level01
    }
}
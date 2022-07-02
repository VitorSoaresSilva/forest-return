using System;
using Attributes;
using ForestReturn.Scripts.Camera;
using Player;
using UI;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Transform[] pointsToSpawn;
        public GameState State;
        public CameraFollow CameraFollow;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
        }

        private void GameManagerOnOnGameStateChanged(GameState obj)
        {
            if (obj == State)
            {
                SpawnPlayer();
            }

            if (State == GameState.Level01 || State == GameState.Lobby)
            {
                GameManager.instance.PlayerMain.gameObject.SetActive(false);
            }


            if (State == GameState.Lobby)
            {
                if (GameManager.instance.configLobby.blacksmithSaved == true)
                {
                    SpawnBlackSmith();
                }
            }
            if (State == GameState.Level01)
            {
                if (GameManager.instance.configLobby.blacksmithSaved == true)
                {
                    var rato = FindObjectOfType<ratoScript>();
                    Destroy(rato.gameObject);
                }
            }
        }

        private void SpawnBlackSmith()
        {
            var blacksmith = Instantiate(GameManager.instance.configLobby.blackSmithPrefab,
                GameManager.instance.configLobby.blackSmithPosition, quaternion.identity);
        }

        public void SpawnPlayer()
        {
            var player = Instantiate(GameManager.instance.playerPrefab,
                pointsToSpawn[Random.Range(0, pointsToSpawn.Length)].position, Quaternion.identity);
            var playerScript = player.GetComponent<PlayerMain>();
            GameManager.instance.PlayerMain = playerScript;
            
            // setar os valores
            
            
            
            // playerScript.attributes[(int)AttributeType.Health].CurrentValue -= 20;
            CameraFollow.target = player.transform;
            CameraFollow.SetPosition();
            CameraFollow.enabled = true;
        }

        public void UiMenuOpen()
        {
            if (GameManager.instance.GameState != GameState.Lobby)
            {
                UiManager.instance.menuLevels.gameObject.SetActive(true);
            }
            else
            {
                UiManager.instance.menuLobby.gameObject.SetActive(true);
            }
        }
    }
}
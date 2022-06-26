using System;
using Attributes;
using ForestReturn.Scripts.Camera;
using Player;
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

            if (State == GameState.Level01)
            {
                GameManager.instance.PlayerMain.gameObject.SetActive(false);
            }
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
    }
}
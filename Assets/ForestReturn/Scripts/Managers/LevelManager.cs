using System;
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
            // player.SetActive(false);
            GameManager.instance.PlayerMain = player.GetComponent<PlayerMain>();
            CameraFollow.target = player.transform;
            CameraFollow.SetPosition();
            CameraFollow.enabled = true;
            // var camera = Instantiate(GameManager.instance.cameraPrefab,
            //     pointsToSpawn[Random.Range(0, pointsToSpawn.Length)].position, Quaternion.identity);
            //
            // camera.TryGetComponent<CameraFollow>(out var cameraScript);
            // if (cameraScript == null)
            // {
            //     cameraScript = camera.AddComponent<CameraFollow>();
            // }
            // cameraScript.target = player.transform;
            // cameraScript.SetPosition();
        }
    }
}
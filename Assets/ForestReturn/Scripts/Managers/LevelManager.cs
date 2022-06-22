using System;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Transform[] pointsToSpawn;
        public GameState State;

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
        }

        public void SpawnPlayer()
        {
            // Debug.Log(pointsToSpawn.Length);
            var player = Instantiate(GameManager.instance.playerPrefab,
                pointsToSpawn[Random.Range(0, pointsToSpawn.Length)].position, Quaternion.identity);
            // TODO: Spawn camera
        }
    }
}
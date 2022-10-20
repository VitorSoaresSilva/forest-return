using System;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.PlayerScripts;
using UnityEngine;
using Enums = ForestReturn.Scripts.Utilities.Enums;

namespace ForestReturn.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Enums.Scenes sceneIndex;
        public Vector3 pointToSpawn;
        public GameObject playerPrefab;
        public Player PlayerScript
        {
            get
            {
                if (playerScript != null) return playerScript;
                playerScript = FindObjectOfType<Player>();
                if (playerScript == null)
                {
                    var player = Instantiate(playerPrefab,pointToSpawn,Quaternion.identity);
                    playerScript = player.GetComponent<Player>();
                }
                return playerScript;
            }
        }
        public Player playerScript;

        protected virtual void Start()
        {
            if (GameManager.instance == null || !GameManager.instance.loadingFromCheckpoint) return;
            pointToSpawn = GameManager.instance.generalData.playerPosition;
            GameManager.instance.loadingFromCheckpoint = false;
        }
        
        public void OnResumeGame()
        {
            playerScript._playerInput.SwitchCurrentActionMap("gameplay");
        }

        public void OnPauseGame()
        {
            playerScript._playerInput.SwitchCurrentActionMap("Menu");
        }
    }
}
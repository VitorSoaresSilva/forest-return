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
                if (_playerScript != null) return _playerScript;
                _playerScript = FindObjectOfType<Player>();
                if (_playerScript == null)
                {
                    var player = Instantiate(playerPrefab,pointToSpawn,Quaternion.identity);
                    _playerScript = player.GetComponent<Player>();
                }
                return _playerScript;
            }
        }
        private Player _playerScript;

        protected virtual void Start()
        {
            if (GameManager.instance == null) return;
            if (GameManager.instance.loadingFromCheckpoint)
            {
                pointToSpawn = GameManager.instance.generalData.playerPosition;
                GameManager.instance.loadingFromCheckpoint = false;
            }
            else
            {
                GameManager.instance.Save();
            }
        }
        
        public void OnResumeGame()
        {
            _playerScript._playerInput.SwitchCurrentActionMap("gameplay");
        }

        public void OnPauseGame()
        {
            _playerScript._playerInput.SwitchCurrentActionMap("Menu");
        }
    }
}
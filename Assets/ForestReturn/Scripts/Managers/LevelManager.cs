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
        public GameObject camerasHolderPrefab;
        public Player PlayerScript
        {
            get
            {
                if (_playerScript != null) return _playerScript;
                _playerScript = FindObjectOfType<Player>();
                if (_playerScript != null) return _playerScript;
                var player = Instantiate(playerPrefab,pointToSpawn,Quaternion.identity);
                _playerScript = player.GetComponent<Player>();
                return _playerScript;
            }
        }
        private Player _playerScript;

        public CamerasHolder CamerasHolder
        {
            get
            {
                if (_camerasHolder != null) return _camerasHolder;
                _camerasHolder = FindObjectOfType<CamerasHolder>();
                if (_camerasHolder != null) return _camerasHolder;
                var cameraHolder = Instantiate(camerasHolderPrefab,pointToSpawn,Quaternion.identity);
                _camerasHolder = cameraHolder.GetComponent<CamerasHolder>();
                return _camerasHolder;
            }
        }

        private CamerasHolder _camerasHolder;

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
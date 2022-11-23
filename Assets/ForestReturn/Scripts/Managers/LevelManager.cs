using System;
using ForestReturn.Scripts.PlayerScripts;
using ForestReturn.Scripts.Utilities;
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
        public PlayerManager PlayerScript
        {
            get
            {
                // if (_playerScript != null) return _playerScript;
                // _playerScript = FindObjectOfType<Player>();
                // if (_playerScript != null) return _playerScript;
                // var player = Instantiate(playerPrefab,pointToSpawn,Quaternion.identity);
                // _playerScript = player.GetComponent<Player>();
                // return _playerScript;
                
                if (_playerScript != null) return _playerScript;
                _playerScript = FindObjectOfType<PlayerManager>();
                if (_playerScript != null) return _playerScript;
                var player = Instantiate(playerPrefab,pointToSpawn,Quaternion.identity);
                _playerScript = player.GetComponent<PlayerManager>();
                return _playerScript;
            }
        }
        private PlayerManager _playerScript;

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
            if (!GameManager.InstanceExists) return;
            if (GameManager.Instance.loadingFromCheckpoint)
            {
                pointToSpawn = GameManager.Instance.generalData.playerPosition;
                GameManager.Instance.loadingFromCheckpoint = false;
            }else if (GameManager.Instance.generalData.HasTeleportData)
            {
                if (sceneIndex != Enums.Scenes.Lobby)
                {
                    if (GameManager.Instance.generalData.TeleportScene == sceneIndex)
                    {
                        pointToSpawn = GameManager.Instance.generalData.TeleportPointToSpawn;
                    }
                    GameManager.Instance.generalData.ClearTeleport();
                }
                // GameManager.Instance.Save();
            }
            // else
            // {
            //     GameManager.Instance.Save();
            // }
            
            

            // if (GameManager.Instance.loadingFromCheckpoint)
            // {
            //     pointToSpawn = GameManager.Instance.generalData.playerPosition;
            //     GameManager.Instance.loadingFromCheckpoint = false;
            // }else if (GameManager.Instance.generalData.HasTeleportData())
            // {
            //     if (GameManager.Instance.generalData.teleportScene != Enums.Scenes.Lobby)
            //     {
            //         if (GameManager.Instance.generalData.teleportScene == sceneIndex)
            //         {
            //             pointToSpawn = (Vector3)GameManager.Instance.generalData.teleportPointToSpawn;
            //         }
            //         GameManager.Instance.generalData.ClearTeleport();
            //     }
            //     
            // }
            // else
            // {
            //     GameManager.Instance.Save();
            // }
        }
    }
}
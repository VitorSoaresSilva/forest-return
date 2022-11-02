using System;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    [Serializable]
    [CreateAssetMenu(fileName = "new Game Data", menuName = "Items/GameData")]
    public class GeneralDataObject : ScriptableObject
    {
        // public string path;
        public string lastSaveString;
        public long lastSaveLong;
        public Vector3 playerPosition;
        public Enums.Scenes currentLevel;
        [field: SerializeField] public Vector3 TeleportPointToSpawn {get; private set;}
        [field: SerializeField] public Enums.Scenes TeleportScene {get; private set;}
        [field: SerializeField] public bool HasTeleportData {get; private set;}
        [field: SerializeField] public int PlayerCurrentHealth {get; private set;}
        [field: SerializeField] public int PlayerCurrentMana {get; private set;}
        [field: SerializeField] public bool HasPlayerData {get; private set;}
        public void Init()
        {
            currentLevel = Enums.Scenes.Lobby;
            // ClearTeleport();
            // ClearPlayerData();
        }

        public void Clear()
        {
            lastSaveString = String.Empty;
            lastSaveLong = 0;
            currentLevel = Enums.Scenes.Lobby;
            ClearTeleport();
            ClearPlayerData();
        }

        public void SetTeleportData(TeleportData newTeleportData)
        {
            TeleportPointToSpawn = newTeleportData.teleportPointToSpawn;
            TeleportScene = newTeleportData.teleportScene;
            HasTeleportData = true;
        }

        public void SetPlayerData()
        {
            PlayerCurrentHealth = LevelManager.Instance.PlayerScript.CurrentHealth;
            PlayerCurrentMana = LevelManager.Instance.PlayerScript.CurrentMana;
            HasPlayerData = true;
        }
        public void ClearTeleport()
        {
            TeleportPointToSpawn = Vector3.zero;
            TeleportScene = Enums.Scenes.Lobby;
            HasTeleportData = false;
        }

        public void ClearPlayerData()
        {
            PlayerCurrentHealth = 0;
            PlayerCurrentMana = 0;
            HasPlayerData = false;
        }
    }
}
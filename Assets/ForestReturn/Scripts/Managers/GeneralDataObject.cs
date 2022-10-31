using System;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "new Game Data", menuName = "Items/GameData")]
    public class GeneralDataObject : ScriptableObject
    {
        // public string path;
        public string LastSaveString;
        public long LastSaveLong;
        public Vector3 playerPosition;
        public BaseCharacterData? playerCharacterData;
        
        public Enums.Scenes currentLevel;
        // public Vector3 pointToSpawn;
        [SerializeField]public TeleportData? TeleportData;

        public void Init()
        {
            currentLevel = Enums.Scenes.Lobby;
            TeleportData = null;
        }

        public void Clear()
        {
            LastSaveString = String.Empty;
            LastSaveLong = 0;
            currentLevel = Enums.Scenes.Lobby;
            TeleportData = null;
        }
    }
}
using System;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts
{
    [CreateAssetMenu(fileName = "new Game Data", menuName = "Items/GameData")]
    public class GeneralDataObject : ScriptableObject
    {
        // public string path;
        public string LastSaveString;
        public long LastSaveLong;
        
        public Enums.Scenes currentLevel;
        // public Vector3 pointToSpawn;
        public TeleportData TeleportData;

        public void Init()
        {
            currentLevel = Enums.Scenes.Lobby;
            TeleportData = new TeleportData();
        }

        public void Clear()
        {
            LastSaveString = String.Empty;
            LastSaveLong = 0;
            currentLevel = Enums.Scenes.Level01;
            TeleportData = new TeleportData();
        }
    }
}
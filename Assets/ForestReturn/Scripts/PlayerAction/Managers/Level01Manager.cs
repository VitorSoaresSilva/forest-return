using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class Level01Manager : LevelManager
    {
        private void Start()
        {
            var teleportData = GameManager.instance.gameDataObject.TeleportData;
            if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
            {
                pointToSpawn = teleportData.PositionToSpawn;
                GameManager.instance.gameDataObject.TeleportData.AlreadyReturned = true;
            }
            playerScript.Init();
        }
    }
}
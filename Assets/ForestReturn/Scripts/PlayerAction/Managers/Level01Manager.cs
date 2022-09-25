using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class Level01Manager : LevelManager
    {
        private void Start()
        {
            if (GameManager.instance == null) return;
            var teleportData = GameManager.instance.generalData.TeleportData;
            if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
            {
                pointToSpawn = teleportData.PositionToSpawn;
                GameManager.instance.generalData.TeleportData.AlreadyReturned = true;
            }
            playerScript.Init();
        }
    }
}
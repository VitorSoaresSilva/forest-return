using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level02Manager : LevelManager
    {
        private void Start()
        {
            Debug.Log("level 02");
            if (GameManager.instance != null)
            {
                var teleportData = GameManager.instance.generalData.TeleportData;
                if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
                {
                    pointToSpawn = teleportData.PositionToSpawn;
                    GameManager.instance.generalData.TeleportData.AlreadyReturned = true;
                }
            }

            PlayerScript.Init();
        }
    }
}
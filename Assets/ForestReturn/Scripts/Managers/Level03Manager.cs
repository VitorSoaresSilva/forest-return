using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level03Manager : LevelManager
    {
        protected override void Start()
        {
            base.Start();
            if (GameManager.Instance != null)
            {
                var teleportData = GameManager.Instance.generalData.TeleportData;
                if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
                {
                    pointToSpawn = teleportData.PositionToSpawn;
                    GameManager.Instance.generalData.TeleportData.AlreadyReturned = true;
                }
            }
            PlayerScript.Init();
            if (UiManager.Instance != null)
            {
                UiManager.Instance.Init();
            }
        }
    }
}
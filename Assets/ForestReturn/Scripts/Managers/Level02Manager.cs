using ForestReturn.Scripts.Teleport;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level02Manager : LevelManager
    {
        protected override void Start()
        {
            base.Start();
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.generalData.TeleportData != null)
                {
                    TeleportData teleportData = (TeleportData)GameManager.Instance.generalData.TeleportData;
                    if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
                    {
                        pointToSpawn = teleportData.PositionToSpawn;
                        GameManager.Instance.generalData.TeleportData = null;
                    }
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
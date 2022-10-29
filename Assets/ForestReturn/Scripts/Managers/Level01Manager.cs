using ForestReturn.Scripts.Teleport;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level01Manager : LevelManager
    {
        public Vector3 pointToNpcGoAway;
        protected override void Start()
        {
            base.Start();
            if (GameManager.InstanceExists)
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
            if (UiManager.InstanceExists)
            {
                UiManager.Instance.Init();
            }
        }
    }
}
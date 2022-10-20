using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level01Manager : LevelManager
    {
        public Vector3 pointToNpcGoAway;
        protected override void Start()
        {
            base.Start();
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
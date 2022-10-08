using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class LobbyLevelManager : LevelManager
    {
        public TriggerObject npcSaved;
        public TriggerObject cutsceneWatched;
        public GameObject[] NPCs;
        
        private void Start()
        {
            
            
            
            // if (GameManager.instance.triggerInventory.Contains(cutsceneWatched))
            // {
            //     Debug.Log("Do not play");
            // }
            // else
            // {
            //     Debug.Log("Play cutscene");
            // }
            
            
            if (GameManager.instance != null && GameManager.instance.triggerInventory.Contains(npcSaved))
            {
                foreach (var npC in NPCs)
                {
                    npC.SetActive(true);
                }
            }
            else
            {
                foreach (var npC in NPCs)
                {
                    npC.SetActive(false);
                }
            }

            if (GameManager.instance != null)
            {
                var teleportData = GameManager.instance.generalData.TeleportData;
                if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
                {
                    pointToSpawn = teleportData.PositionToSpawn;
                }
            }
            PlayerScript.Init();
        }
    }
}
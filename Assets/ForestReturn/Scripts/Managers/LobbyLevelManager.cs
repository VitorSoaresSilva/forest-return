using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class LobbyLevelManager : LevelManager
    {
        public TriggerObject npcSaved;
        public TriggerObject cutsceneWatched;
        public GameObject[] NPCs;
        
        protected override void Start()
        {
            base.Start();
            Debug.Log("Lobby");
            if (GameManager.instance != null)
            {
                var npcState = GameManager.instance.triggerInventory.Contains(npcSaved);
                foreach (var npc in NPCs)
                {
                    npc.SetActive(npcState);
                }
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
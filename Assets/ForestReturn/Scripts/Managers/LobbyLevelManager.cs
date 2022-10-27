using ForestReturn.Scripts.NPCs;
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
            if (GameManager.InstanceExists)
            {
                bool npcState = GameManager.Instance.triggerInventory.Contains(npcSaved);
                foreach (var npc in NPCs)
                {
                    npc.SetActive(npcState);
                    npc.GetComponent<IBaseNpc>().InitOnLobby();
                }
                var teleportData = GameManager.Instance.generalData.TeleportData;
                if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
                {
                    pointToSpawn = teleportData.PositionToSpawn;
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
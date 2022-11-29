using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.NPCs;
using ForestReturn.Scripts.Teleport;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class LobbyLevelManager : LevelManager
    {
        public TriggerObject npcSaved;
        public TriggerObject cutsceneWatched;
        public GameObject[] NPCs;
        public Forja forja;
        public Vector3 pointToSpawnIntro;
        public Vector3 pointToSpawnLobby;
        public GameObject doorToIntro;
        
        protected override void Start()
        {
            base.Start();
            if (GameManager.InstanceExists && InventoryManager.InstanceExists)
            {
                bool npcState = InventoryManager.Instance.triggerInventory.Contains(npcSaved);
                forja.ChangeState(npcState);
                doorToIntro.SetActive(npcState);
                pointToSpawn = npcState ? pointToSpawnLobby : pointToSpawnIntro;
                foreach (var npc in NPCs)
                {
                    npc.SetActive(npcState);
                    //npc.GetComponent<IBaseNpc>().InitOnLobby();
                    if (npc.TryGetComponent(out IBaseNpc iBaseNpc))
                    {
                        iBaseNpc.InitOnLobby();
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
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
        
        protected override void Start()
        {
            base.Start();
            if (GameManager.InstanceExists && InventoryManager.InstanceExists)
            {
                bool npcState = InventoryManager.Instance.triggerInventory.Contains(npcSaved);
                foreach (var npc in NPCs)
                {
                    npc.SetActive(npcState);
                    npc.GetComponent<IBaseNpc>().InitOnLobby();
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
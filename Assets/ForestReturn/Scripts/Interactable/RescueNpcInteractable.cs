using _Developers.Vitor.Scripts.Interactable;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Interactable
{
    public class RescueNpcInteractable : MonoBehaviour, IInteractable
    {
        public GameObject[] npcGameObjects;
        public TriggerObject npcRescued;

        private void Start()
        {
            if (GameManager.instance != null && GameManager.instance.triggerInventory.Contains(npcRescued))
            {
                foreach (var npcGameObject in npcGameObjects)
                {
                    Destroy(npcGameObject);
                }
            }
        }

        public void Interact()
        {
            
            //Todo: Get npc NavMeshAgent and set to the begin of level
            foreach (var npcGameObject in npcGameObjects)
            {
                Destroy(npcGameObject);
            }
            GameManager.instance.triggerInventory.AddTrigger(npcRescued);
        }
    }
}

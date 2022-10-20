using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.AI;

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
                npcGameObject.TryGetComponent(out NavMeshAgent navMeshAgent);
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(((Level01Manager)LevelManager.instance).pointToNpcGoAway);
                navMeshAgent.stoppingDistance = 0;
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.triggerInventory.AddTrigger(npcRescued);
            }
        }

        public void SetStatusInteract(bool status)
        {
        }
    }
}

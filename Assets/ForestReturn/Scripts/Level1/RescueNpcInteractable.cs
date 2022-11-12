using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Level1
{
    public class RescueNpcInteractable : MonoBehaviour, IInteractable
    {
        public GameObject[] npcGameObjects;
        public TriggerObject npcRescued;
        public TriggerObject keyCage;
        public UnityEvent onInteractableTrue;
        public UnityEvent onInteractableFalse;
        public UnityEvent onKeyNeededTrue;
        public UnityEvent onKeyNeededFalse;

        private void Start()
        {
            if (InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(npcRescued))
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
                navMeshAgent.SetDestination(((Level01Manager)LevelManager.Instance).pointToNpcGoAway);
                navMeshAgent.stoppingDistance = 0;
            }

            if (InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.triggerInventory.AddTrigger(npcRescued);
            }
        }

        public void SetStatusInteract(bool status)
        {
            if (InventoryManager.Instance.triggerInventory.Contains(keyCage))
            {
                if (status)
                {
                    onInteractableTrue?.Invoke();
                }
                else
                {
                    onInteractableFalse?.Invoke();
                }
            }
            else
            {
                if (status)
                {
                    onKeyNeededTrue?.Invoke();
                }
                else
                {
                    onKeyNeededFalse?.Invoke();
                }
            }
                
                
        }
    }
}

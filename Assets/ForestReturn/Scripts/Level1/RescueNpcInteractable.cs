using System;
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
        [SerializeField] private NpcRescueManager npcRescueManager; 
        public TriggerObject keyCage;
        public UnityEvent onInteractableTrue;
        public UnityEvent onInteractableFalse;
        public UnityEvent onKeyNeededTrue;
        public UnityEvent onKeyNeededFalse;
        [SerializeField] private NavMeshAgent navMeshAgent;
        

        public void Interact()
        {
            if (InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(keyCage))
            {
                npcRescueManager.OnEnemyKilled += OnEnemyKilled;
                npcRescueManager.Rescue();
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(((Level01Manager)LevelManager.Instance).pointToNpcGoAway[0]);
                navMeshAgent.stoppingDistance = 1;
            }
        }

        private void OnEnemyKilled()
        {
            navMeshAgent.SetDestination(((Level01Manager)LevelManager.Instance).pointToNpcGoAway[1]);
            navMeshAgent.stoppingDistance = 1;
        }

        private void OnDestroy()
        {
            npcRescueManager.OnEnemyKilled -= OnEnemyKilled;
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

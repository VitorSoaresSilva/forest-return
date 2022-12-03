using System;
using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.NPCs;
using ForestReturn.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts
{
    public class PortalExitLevel : MonoBehaviour, IInteractable
    {
        public UnityEvent SetAsInteractable;
        public UnityEvent SetAsNotInteractable;
        public TriggerObject levelCompleteTrigger;

        public void Interact()
        {
            InventoryManager.Instance.triggerInventory.AddTrigger(levelCompleteTrigger);
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.ChangeScene(Enums.Scenes.Lobby);
                
            }
        }

        public void SetStatusInteract(bool status)
        {
            if (status)
            {
                SetAsInteractable.Invoke();
            }
            else
            {
                SetAsNotInteractable.Invoke();
            }
        }
    }
}
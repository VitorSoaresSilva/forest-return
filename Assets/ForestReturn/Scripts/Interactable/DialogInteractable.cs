using System;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Interactable
{
    public class DialogInteractable : MonoBehaviour, IInteractable
    {
        public GameObject alert;
        public string dialogMessage;
        public TriggerObject[] triggerObject;
        public bool updatePlayerCapsule;
        public TriggerObject[] triggersNeededToShow;
        public TriggerObject[] triggerToHide;
        private void Start()
        {
            foreach (var trigger in triggerToHide)
            {
                if (InventoryManager.Instance.triggerInventory.Contains(trigger))
                {
                    Destroy(gameObject);
                    return;
                }
            }
            foreach (var trigger in triggersNeededToShow)
            {
                if (!InventoryManager.Instance.triggerInventory.Contains(trigger))
                {
                    Destroy(gameObject);
                    return;
                }
            }
            
        }

        public void Interact()
        {
            UiManager.Instance.SetDialogText(dialogMessage);
            if (InventoryManager.InstanceExists 
                && triggerObject != null 
                && !ContainsAll())
            {
                foreach (var trigger in triggerObject)
                {
                    InventoryManager.Instance.triggerInventory.AddTrigger(trigger);
                }
                if (updatePlayerCapsule)
                {
                    LevelManager.Instance.PlayerScript.UpdateCapsule();
                }
            }

           
        }

        public void SetStatusInteract(bool status)
        {
            if (alert != null)
            {
                alert.SetActive(status);
            }
        }

        private bool ContainsAll()
        {
            foreach (var trigger in triggerObject)
            {
                if(!InventoryManager.Instance.triggerInventory.Contains(trigger))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Interactable
{
    public class ItemCollectable : MonoBehaviour,IInteractable
    {
        public ItemObject itemObject;
        public GameObject alert;
        public int quantity = 1;
        public bool isUnique;
        public TriggerObject triggerObject;

        private void Start()
        {
            if (InventoryManager.InstanceExists && triggerObject &&
                InventoryManager.Instance.triggerInventory.Contains(triggerObject))
            {
                Destroy(gameObject);
            }
            SetStatusInteract(false);
        }

        
        public void Interact()
        {
            if (InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.inventory.AddItem(itemObject,quantity);
                if (isUnique && triggerObject != null)
                {
                    InventoryManager.Instance.triggerInventory.AddTrigger(triggerObject);
                }
                Destroy(gameObject);
            }
            
        }

        public void SetStatusInteract(bool status)
        {
            if (alert != null)
            {
                alert.SetActive(status);
            }
        }
    }
}
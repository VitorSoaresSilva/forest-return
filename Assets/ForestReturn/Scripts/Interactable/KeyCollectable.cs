using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Interactable
{
    public class KeyCollectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyObject key;
        public GameObject alert;

        private void Start()
        {
            SetStatusInteract(false);
            if (InventoryManager.InstanceExists && key.keyTriggerObject &&
                InventoryManager.Instance.triggerInventory.Contains(key.keyTriggerObject))
            {
                Destroy(gameObject);
            }
            
        }
        
        public void Interact()
        {
            if (InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.inventory.AddItem(key);
                if (key.isUnique && key.keyTriggerObject != null)
                {
                    InventoryManager.Instance.triggerInventory.AddTrigger(key.keyTriggerObject);
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
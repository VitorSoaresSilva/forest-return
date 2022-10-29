using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts
{
    public class ItemCollectable : MonoBehaviour,IInteractable
    {
        public ItemObject itemObject;
        public GameObject alert;
        public int quantity = 1;

        private void Start()
        {
            SetStatusInteract(false);
        }

        public void Interact()
        {
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.inventory.AddItem(itemObject,quantity);
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
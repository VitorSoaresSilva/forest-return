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
        private bool _isAlertNotNull;

        private void Start()
        {
            _isAlertNotNull = alert != null;
            SetStatusInteract(false);
        }

        public void Interact()
        {
            if (InventoryManager.instance != null)
            {
                InventoryManager.instance.inventory.AddItem(itemObject,quantity);
                Destroy(gameObject);
            }
            
        }

        public void SetStatusInteract(bool status)
        {
            if (_isAlertNotNull)
            {
                alert.SetActive(true);
            }
        }
    }
}
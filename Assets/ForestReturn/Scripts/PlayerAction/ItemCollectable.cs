using ForestReturn.Scripts.PlayerAction.Inventory;
using Interactable;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class ItemCollectable : MonoBehaviour,IInteractable
    {
        public ItemObject itemObject;
        public GameObject alert;
        public int quantity = 1;
        public void Interact()
        {
            InventoryManager.instance.inventory.AddItem(itemObject,quantity);
            Destroy(gameObject);
        }

        public void SetAsInteract()
        {
            alert.SetActive(true);
        }
    }
}
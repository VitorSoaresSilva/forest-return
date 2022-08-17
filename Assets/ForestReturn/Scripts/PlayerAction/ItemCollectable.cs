using ForestReturn.Scripts.PlayerAction.Inventory;
using Interactable;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class ItemCollectable : MonoBehaviour,IInteractable
    {
        public ItemObject ItemObject;
        public GameObject alert;
        public void Interact()
        {
            InventoryManager.instance.inventoryObject.AddItem(ItemObject,ItemObject.quantityPerPack);
            Destroy(gameObject);
        }

        public void SetAsInteract()
        {
            alert.SetActive(true);
        }
    }
}
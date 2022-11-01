using System.Collections.Generic;
using ForestReturn.Scripts.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class DisplayInventory : MonoBehaviour
    {
        public GameObject prefab;
        public Transform grid;
        private Dictionary<InventorySlotUI, InventorySlot> itemsDisplayed = new();
        public InventoryItemDescription inventoryItemDescription;

        private void OnEnable()
        {
            CreateSlots();
        }

        private void CreateSlots()
        {
            itemsDisplayed = new();
            for (int i = 0; i < InventoryManager.Instance.inventory.Items.Count; i++)
            {
                var itemUI = Instantiate(prefab,grid);
                var inventorySlotUI = itemUI.GetComponent<InventorySlotUI>();
                if (itemUI != null)
                {
                    inventorySlotUI.UpdateData(InventoryManager.Instance.inventory.Items[i], this);
                    itemsDisplayed.Add(inventorySlotUI, InventoryManager.Instance.inventory.Items[i]);
                }
            }
        }

        private void OnDisable()
        {
            var childCount = grid.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(grid.GetChild(i).gameObject);
            }
        }

        public void SetAsSelected(InventorySlot inventorySlot)
        {
            inventoryItemDescription.UpdateData(inventorySlot);
        }
        
    }
}
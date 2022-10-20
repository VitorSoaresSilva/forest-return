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

        private void OnEnable()
        {
            CreateSlots();
        }

        private void CreateSlots()
        {
            itemsDisplayed = new();
            for (int i = 0; i < InventoryManager.instance.inventory.Items.Count; i++)
            {
                var itemUI = Instantiate(prefab,grid);
                var inventorySlotUI = itemUI.GetComponent<InventorySlotUI>();
                if (itemUI != null)
                {
                    inventorySlotUI.UpdateData(InventoryManager.instance.inventory.Items[i]);
                    itemsDisplayed.Add(inventorySlotUI, InventoryManager.instance.inventory.Items[i]);
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
    }
}
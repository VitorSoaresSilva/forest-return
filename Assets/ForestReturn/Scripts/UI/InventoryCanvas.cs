using System.Collections.Generic;
using System.Linq;
using ForestReturn.Scripts.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ForestReturn.Scripts.UI
{
    public class InventoryCanvas : MonoBehaviour
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
            GameObject a = new GameObject();
            itemsDisplayed = new();
            for (int i = 0; i < InventoryManager.Instance.inventory.Items.Count; i++)
            {
                var itemUI = Instantiate(prefab,grid);
                if (i == 0)
                {
                    a = itemUI;
                }
                var inventorySlotUI = itemUI.GetComponent<InventorySlotUI>();
                if (itemUI != null)
                {
                    inventorySlotUI.UpdateData(InventoryManager.Instance.inventory.Items[i], this);
                    itemsDisplayed.Add(inventorySlotUI, InventoryManager.Instance.inventory.Items[i]);
                }
            }
            EventSystem.current.SetSelectedGameObject(a);
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

        public void Deselect()
        {
            inventoryItemDescription.Deselect();
        }
        
    }
}
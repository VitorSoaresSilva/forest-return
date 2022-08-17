using System;
using ForestReturn.Scripts.PlayerAction.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.UI
{
    public class DisplayInventory : MonoBehaviour
    {
        private InventoryObject _inventoryObject;
        public GameObject prefab;
        public Transform grid;

        private void OnEnable()
        {
            _inventoryObject = InventoryManager.instance.inventoryObject;
            for (int i = 0; i < _inventoryObject.Container.Count; i++)
            {
                var itemUI = Instantiate(prefab,grid);
                var inventorySlotUI = itemUI.GetComponent<InventorySlotUI>();
                if (itemUI != null)
                {
                    inventorySlotUI.UpdateData(_inventoryObject.Container[i]);
                }
            }
        }

        private void OnDisable()
        {
            var childCount = grid.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(grid.GetChild(i).gameObject);
            }
        }
    }
}
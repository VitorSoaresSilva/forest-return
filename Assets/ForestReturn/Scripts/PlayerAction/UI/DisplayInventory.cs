using System;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.UI
{
    public class DisplayInventory : MonoBehaviour
    {
        private InventoryObject _inventoryObject;
        public GameObject prefab;
        public Transform grid;
        private Dictionary<InventorySlotUI, InventorySlot> itemsDisplayed = new();

        private void Start()
        {
            _inventoryObject = InventoryManager.instance.inventoryObject;
            CreateSlots();
        }

        private void OnEnable()
        {
            CreateSlots();
        }

        private void CreateSlots()
        {
            itemsDisplayed = new();
            for (int i = 0; i < _inventoryObject.Container.Items.Count; i++)
            {
                var itemUI = Instantiate(prefab,grid);
                var inventorySlotUI = itemUI.GetComponent<InventorySlotUI>();
                if (itemUI != null)
                {
                    inventorySlotUI.UpdateData(_inventoryObject.Container.Items[i]);
                    itemsDisplayed.Add(inventorySlotUI, _inventoryObject.Container.Items[i]);
                }
            }
        }

        private void Update()
        {
            // for (int i = 0; i < _inventoryObject.Container.Items.Count; i++)
            // {
            //     
            // }
            // foreach (KeyValuePair<InventorySlotUI, InventorySlot> _slot in itemsDisplayed)
            // {
            //     Debug.Log(_slot.Key);
            //     if (_slot.Value.id >= 0)
            //     {
            //         _slot.Key.UpdateData(_inventoryObject.Container.Items[_slot.Value.item.id]);
            //     }
            // }
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
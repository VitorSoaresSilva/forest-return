using System;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Inventory;
using ForestReturn.Scripts.PlayerAction.Managers;
using ForestReturn.Scripts.PlayerAction.UI;
using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        public InventoryObject inventory;
        public InventoryObject equippedItems;
        public ItemDatabaseObject Database;
        [SerializeField] private DisplayInventory displayInventory;

        private void Start()
        {
            Load();
        }

        public void OnApplicationQuit()
        {
            inventory.Clear();
            equippedItems.Clear();
        }

        public void OpenInventory()
        {
            displayInventory.gameObject.SetActive(true);
        }

        public void CloseInventory()
        {
            displayInventory.gameObject.SetActive(false);
        }

        public void Save()
        {
            inventory.path = GameManager.instance.InventorySavePath;
            equippedItems.path = GameManager.instance.EquippedSavePath;
            inventory.Save();
            equippedItems.Save();
        }

        public void Load()
        {
            inventory.path = GameManager.instance.InventorySavePath;
            equippedItems.path = GameManager.instance.EquippedSavePath;
            inventory.Load();
            equippedItems.Load();
        }
    }
}
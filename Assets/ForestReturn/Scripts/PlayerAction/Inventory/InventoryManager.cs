using System;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Inventory;
using ForestReturn.Scripts.PlayerAction.UI;
using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        public InventoryObject inventory;
        public ItemDatabaseObject Database;
        [SerializeField] private DisplayInventory displayInventory;
        public string savePath;
        protected override void Awake()
        {
            base.Awake();
            inventory.Load();
        }

        public void OnApplicationQuit()
        {
            inventory.Clear();
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
            inventory.Save();
        }
    }
}
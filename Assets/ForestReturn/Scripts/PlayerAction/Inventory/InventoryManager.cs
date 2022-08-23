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
        public InventoryObject inventoryObject;
        [field: SerializeField] public ItemDatabaseObject Database { get;}
        [SerializeField] private DisplayInventory displayInventory;
        public string savePath;
        protected override void Awake()
        {
            base.Awake();
            inventoryObject.Load();
        }

        public void OnApplicationQuit()
        {
            inventoryObject.Clear();
        }

        public void OpenInventory()
        {
            displayInventory.gameObject.SetActive(true);
        }

        public void CloseInventory()
        {
            displayInventory.gameObject.SetActive(false);
        }
    }
}
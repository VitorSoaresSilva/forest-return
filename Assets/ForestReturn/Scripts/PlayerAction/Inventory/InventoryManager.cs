using System;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Inventory;
using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction
{
    public class InventoryManager : PersistentSingleton<InventoryManager>
    {
        public InventoryObject inventoryObject;

        protected override void Awake()
        {
            base.Awake();
            // inventoryObject = new InventoryObject();
            inventoryObject.Load();
        }

        public void OnApplicationQuit()
        {
            inventoryObject.Container.Items.Clear();
        }
    }
}
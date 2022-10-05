using System;
using ForestReturn.Scripts.PlayerAction.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.NPCs
{
    public class BlacksmithStore : MonoBehaviour
    {
        private ItemObject[] items;

        private void OnEnable()
        {
            // items = InventoryManager.instance.inventory.GetItemsByTypes()
        }
    }
}
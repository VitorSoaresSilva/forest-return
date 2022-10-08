using ForestReturn.Scripts.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts.NPCs
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
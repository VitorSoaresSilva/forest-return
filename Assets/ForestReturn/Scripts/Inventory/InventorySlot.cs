using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public int id;
        public ItemObject item;
        public int amount;
        public int level;

        public InventorySlot(int id, int amount, ItemObject item)
        {
            this.id = id;
            this.amount = amount;
            this.item = item;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }

        public void RemoveAmount(int value)
        {
            amount = Mathf.Max(0, amount - value);
        }
    }
}
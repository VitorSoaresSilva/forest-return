using System;
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [Serializable]
    public class MaskInventorySlot : InventorySlot
    {
        public MaskInventorySlot(int id, int amount, ItemObject item) : base(id, amount, item)
        {
            
        }
        public MaskInventorySlot(int id, ItemObject item) : base(id, 1, item)
        {
            
        }
        
    }
}
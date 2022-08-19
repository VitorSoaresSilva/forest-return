using System.Collections.Generic;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> Items = new ();
    }
}
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [System.Serializable]
    public class SwordInventorySlot : InventorySlot
    {
        public int slotsAmount;
        
        public SwordInventorySlot(int id, int amount, ItemObject item, int slotsAmount) : base(id, amount, item)
        {
            this.slotsAmount = slotsAmount;
        }
        public SwordInventorySlot(int id, ItemObject item, int slotsAmount) : base(id, 1, item)
        {
            this.slotsAmount = slotsAmount;
        }

        public void AddSlot()
        {
            /* Slot 0 and 1 can be created without the Special Hammer 
             */
            //TODO: Talvez adicionar um evento de quando der certo de Add slots
            slotsAmount = Mathf.Min(slotsAmount + 1, 3);
        }
    }
}
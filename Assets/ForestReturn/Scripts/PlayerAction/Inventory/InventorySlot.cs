namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public int id;
        public ItemObject item;
        public int amount;

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
    }
}
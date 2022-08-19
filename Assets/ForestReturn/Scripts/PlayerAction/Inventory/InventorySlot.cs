namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public int id;
        public Item item;
        public int amount;

        public InventorySlot(int id, int amount, Item item)
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
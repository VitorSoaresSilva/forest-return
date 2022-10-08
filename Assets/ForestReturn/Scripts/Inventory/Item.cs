namespace ForestReturn.Scripts.Inventory
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public int id;

        public Item(ItemObject item)
        {
            name = item.name;
            id = item.id;
        }
        
    }
}
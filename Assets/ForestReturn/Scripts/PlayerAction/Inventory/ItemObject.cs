using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    public class ItemObject : ScriptableObject
    {
        public int id;
        public string itemName;
        public GameObject prefab;
        public Texture image;
        public int price;
        public bool isStackable;
        public int quantityPerPack;
        public ItemType itemType;
    }

    public enum ItemType
    {
        Default,
        Potion,
        Weapon,
    }
}
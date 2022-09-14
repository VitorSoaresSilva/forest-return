using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    public class ItemObject : ScriptableObject
    {
        public int id;
        public string itemName;
        public GameObject prefab;
        public Sprite image;
        public bool isStackable;
        public ItemType itemType;
    }

    public enum ItemType
    {
        Default,
        Potion,
        Weapon,
        Currency
    }

    public enum PotionType
    {
        Life,
        Mana,
    }

    public enum CurrencyType
    {
        Seed,
        Scrap,
        Essence
    }
}
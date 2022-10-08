using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "newPotion", menuName = "Items/Potion", order = 0)]
    public class PotionObject : ItemObject
    {
        public int value;
        public PotionType potionType;
        private void Awake()
        {
            isStackable = true;
            itemType = ItemType.Potion;
            isUnique = false;
            hasLevels = false;
        }
    }
}
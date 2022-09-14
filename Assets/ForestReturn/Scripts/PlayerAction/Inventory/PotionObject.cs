using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
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
        }
    }
}
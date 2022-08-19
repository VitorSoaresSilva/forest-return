using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "newWeapon", menuName = "Items/Weapon", order = 0)]
    public class WeaponObject : ItemObject
    {
        public int minDamage;
        public int maxDamage;

        public void Awake()
        {
            isStackable = false;
            quantityPerPack = 1;
            itemType = ItemType.Weapon;
        }

    }
}
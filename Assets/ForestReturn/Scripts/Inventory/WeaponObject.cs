using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "newWeapon", menuName = "Items/Weapon", order = 0)]
    public class WeaponObject : ItemObject
    {
        public int minDamage;
        public int maxDamage;

        public void Awake()
        {
            isStackable = false;
            itemType = ItemType.Weapon;
            hasLevels = true;
            isUnique = true;
        }

    }
}
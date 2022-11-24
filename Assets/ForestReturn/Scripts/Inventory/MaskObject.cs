using System;
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "newMask", menuName = "Items/Mask")]
    public class MaskObject : ItemObject
    {
        public void Awake()
        {
            isStackable = false;
            itemType = ItemType.Mask;
            hasLevels = true;
            isUnique = true;
        }
    }
}
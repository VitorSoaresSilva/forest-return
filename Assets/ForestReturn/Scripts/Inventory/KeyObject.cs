using System;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "newKey", menuName = "Items/Keys")]
    public class KeyObject : ItemObject
    {
        public TriggerObject keyTriggerObject;

        private void Awake()
        {
            isStackable = false;
            itemType = ItemType.Key;
            isUnique = true;
            hasLevels = false;
        }
        
        
    }
}

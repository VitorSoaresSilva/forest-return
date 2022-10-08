using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "newTeleport", menuName = "Items/Teleport")]
    public class TeleportObject : ItemObject
    {
        public void Awake()
        {
            isStackable = true;
            itemType = ItemType.Teleport;
            hasLevels = false;
            isUnique = false;
        }

    }
}
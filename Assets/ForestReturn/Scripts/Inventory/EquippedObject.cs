using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "new Equipped Inventory", menuName = "Items/Equipped Inventory")]
    public class EquippedObject : ScriptableObject
    {
        public SwordInventorySlot swordInventorySlot;
        public MaskInventorySlot maskInventorySlot;
        public InventorySlot[] artifacts = new InventorySlot[3];
        public void EquipWeapon(SwordInventorySlot newSwordInventorySlot)
        {
            if (newSwordInventorySlot.item.itemType == ItemType.Weapon)
            {
                // unequip weapon, this game has only one weapon
                swordInventorySlot = newSwordInventorySlot;
            }
        }

        public void EquipMask(MaskInventorySlot newMaskInventorySlot)
        {
            if (newMaskInventorySlot.item.itemType == ItemType.Mask)
            {
                MaskObject maskObject = (MaskObject)newMaskInventorySlot.item;
                maskInventorySlot = newMaskInventorySlot;
            }
        }

        public void UnEquipMask()
        {
            if (maskInventorySlot != null)
            {
                //TODO: unEquip mask
            }
        }

        public void Init() // new game
        {
            //find weapon - Our game will game only one weapon
            foreach (var databaseItem in InventoryManager.Instance.Database.items)
            {
                if (databaseItem.itemType != ItemType.Weapon) continue;
                
                swordInventorySlot = new SwordInventorySlot(databaseItem.id, 1, databaseItem, 0);
                break;
            }
            
            maskInventorySlot = null;
        }

        public void Load()
        {
            var item = InventoryManager.Instance.Database.items[swordInventorySlot.id];
            // EquipWeapon(new SwordInventorySlot(swordInventorySlot.id,item,swordInventorySlot.slotsAmount));
                // swordInventorySlot.item = InventoryManager.Instance.Database.items[swordInventorySlot.id];
            

            
            EquipMask(new MaskInventorySlot(maskInventorySlot.id,InventoryManager.Instance.Database.items[maskInventorySlot.id]));
            // maskInventorySlot.item = InventoryManager.Instance.Database.items[maskInventorySlot.id];
            
        }

        public void Clear()
        {
            swordInventorySlot = null;
            maskInventorySlot = null;
        }
    }
}
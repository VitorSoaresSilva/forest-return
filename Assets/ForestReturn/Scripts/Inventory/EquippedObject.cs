using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "new Equipped Inventory", menuName = "Items/Equipped Inventory")]
    public class EquippedObject : ScriptableObject
    {
        public SwordInventorySlot swordInventorySlot;
        [SerializeField] private WeaponObject initialWeapon;
        public void EquipWeapon(SwordInventorySlot newSwordInventorySlot)
        {
            if (newSwordInventorySlot.item.itemType == ItemType.Weapon)
            {
                WeaponObject weaponObject = (WeaponObject)newSwordInventorySlot.item;
                swordInventorySlot = newSwordInventorySlot;
            }
        }

        public void Init()
        {
            // SwordInventorySlot a = new SwordInventorySlot(swordInventorySlot.id,);
            EquipWeapon(new SwordInventorySlot(initialWeapon.id,1,initialWeapon,0));
            // swordInventorySlot.item = InventoryManager.Instance.Database.items[swordInventorySlot.id];
        }

        public void Clear()
        {
            swordInventorySlot = null;
        }
    }
}
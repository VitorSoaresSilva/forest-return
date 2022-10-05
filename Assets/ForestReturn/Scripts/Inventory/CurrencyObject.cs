using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "newCurrency", menuName = "Items/Currency", order = 0)]
    public class CurrencyObject : ItemObject
    {
        public CurrencyType type;
        public void Awake()
        {
            isStackable = true;
            isUnique = false;
            itemType = ItemType.Currency;
            hasLevels = false;
        }
    }
}
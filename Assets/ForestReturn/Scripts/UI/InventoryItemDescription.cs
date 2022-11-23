using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class InventoryItemDescription : MonoBehaviour
    {
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
    
        public void UpdateData(InventorySlot itemObject)
        {
            textName.text = itemObject.item.itemName;
            textDescription.text = itemObject.item.itemDescription;
        }

        public void Deselect()
        {
            textName.text = string.Empty;
            textDescription.text = string.Empty;
        }
    }
}

using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class InventoryItemDescription : MonoBehaviour
    {
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
        public Image image;
    
        public void UpdateData(InventorySlot itemObject)
        {
            textName.text = itemObject.item.itemName;
            textDescription.text = itemObject.item.itemDescription;
            image.sprite = itemObject.item.image;
        }

        public void Deselect()
        {
            textName.text = string.Empty;
            textDescription.text = string.Empty;
            image.sprite = null;
        }
    }
}

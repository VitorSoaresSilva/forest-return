using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI amountText;
        [SerializeField]private TextMeshProUGUI nameText;
        [SerializeField] private Image image;
        public void UpdateData(InventorySlot itemObject)
        {
            nameText.text = itemObject.item.name;
            if (itemObject.item.image != null)
            {
                image.sprite = itemObject.item.image;
            }
            amountText.text = itemObject.item.isStackable ? itemObject.amount.ToString() : string.Empty;
        }
    }
}
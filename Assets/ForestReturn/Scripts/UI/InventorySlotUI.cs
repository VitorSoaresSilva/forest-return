using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class InventorySlotUI : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField]private TextMeshProUGUI amountText;
        [SerializeField]private TextMeshProUGUI nameText;
        [SerializeField]private Image image;
        [SerializeField]private Button button;
        private InventoryCanvas _inventoryCanvas;
        private InventorySlot _itemObject;
        public void UpdateData(InventorySlot itemObject, InventoryCanvas inventoryCanvas)
        {
            _inventoryCanvas = inventoryCanvas;
            _itemObject = itemObject;
            nameText.text = itemObject.item.name;
            if (itemObject.item.image != null)
            {
                image.sprite = itemObject.item.image;
            }
            amountText.text = itemObject.item.isStackable ? itemObject.amount.ToString() : string.Empty;
            button.onClick.AddListener(() => {_inventoryCanvas.SetAsSelected(itemObject);});
        }

        public void OnSelect(BaseEventData eventData)
        {
            _inventoryCanvas.SetAsSelected(_itemObject);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _inventoryCanvas.Deselect();
        }
    }
}
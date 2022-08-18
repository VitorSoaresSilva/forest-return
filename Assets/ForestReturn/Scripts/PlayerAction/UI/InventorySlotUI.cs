using System;
using ForestReturn.Scripts.PlayerAction.Inventory;
using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI amountText;
        [SerializeField]private TextMeshProUGUI nameText;
        public void UpdateData(InventorySlot itemObject)
        {
            nameText.text = itemObject.item.name;
            // if (itemObject.item.isStackable)
            // {
            amountText.text = itemObject.amount.ToString();
            // }
            // else
            // {
            //     amountText.text = String.Empty;
            // }
        }
    }
}
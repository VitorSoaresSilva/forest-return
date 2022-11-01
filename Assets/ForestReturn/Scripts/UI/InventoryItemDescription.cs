using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;

public class InventoryItemDescription : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    
    public void UpdateData(InventorySlot itemObject)
    {
        textName.text = itemObject.item.itemName;
        textDescription.text = itemObject.item.itemDescription;
    }
}

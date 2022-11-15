using System;
using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI seedText;
        [SerializeField] private TextMeshProUGUI scrapText;

        private void OnEnable()
        {
            
            var seed = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Seed);
            seedText.text = seed!=null ? $"{seed.amount}" : "0";
            var scrap = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Scrap);
            scrapText.text = scrap!=null ? $"{scrap.amount}" : "0";
        }
    }
}
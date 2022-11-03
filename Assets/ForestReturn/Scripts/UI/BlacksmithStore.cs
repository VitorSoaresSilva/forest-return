using System;
using ForestReturn.GameConfig;
using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class BlacksmithStore : MonoBehaviour
    {
        private ItemObject[] items;
        public Button upgradeSlotsButton;
        public Button upgradeWeaponButton;

        public TextMeshProUGUI costWeaponText;
        public void OnEnable()
        {
            UpdateData();
        }


        public void UpdateData()
        {
            var currentWeapon = InventoryManager.Instance.equippedItems.swordInventorySlot;
            var currentCostLevel = BlacksmithConfigData.LevelsCost[Mathf.Min(currentWeapon.level,BlacksmithConfigData.LevelsCost.Length)];   
            costWeaponText.text = $"Seed: {currentCostLevel.SeedCost} - Scrap: {currentCostLevel.ScrapCost}";

            if (CanUpgradeWeapon())
            {
                Debug.Log("Can upgrade");
            }
            else
            {
                Debug.Log("Cannot Upgrade");
            }
            
            
            
        }

        [ContextMenu("Upgrade weapon")]
        public void UpgradeWeapon()
        {
            Debug.Log("Upgrade");
            if (CanUpgradeWeapon())
            {
                Debug.Log("if Upgrade");
                var seed = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Seed);
                var currentWeapon = InventoryManager.Instance.equippedItems.swordInventorySlot;
                var currentCostLevel = BlacksmithConfigData.LevelsCost[Mathf.Min(currentWeapon.level,BlacksmithConfigData.LevelsCost.Length)];
                var scrap = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Scrap);
                seed.RemoveAmount(currentCostLevel.SeedCost);
                scrap.RemoveAmount(currentCostLevel.ScrapCost);
                currentWeapon.level++;
            }
        }

        public bool CanUpgradeWeapon()
        {
            var currentWeapon = InventoryManager.Instance.equippedItems.swordInventorySlot;
            var currentCostLevel = BlacksmithConfigData.LevelsCost[Mathf.Min(currentWeapon.level,BlacksmithConfigData.LevelsCost.Length)];

            var seed = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Seed);
            var hasEnoughSeed = seed != null && seed.amount >= currentCostLevel.SeedCost;
            var scrap = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Scrap);
            var hasEnoughScrap = scrap != null && scrap.amount >= currentCostLevel.ScrapCost;
            
            return hasEnoughScrap && hasEnoughSeed;
        }
        
    }
}
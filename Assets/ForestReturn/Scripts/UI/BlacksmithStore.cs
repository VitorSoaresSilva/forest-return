using System;
using ForestReturn.GameConfig;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class BlacksmithStore : MonoBehaviour
    {
        // private ItemObject[] items;
        public Button upgradeSlotsButton;
        public Button upgradeWeaponButton;

        private InventorySlot _seed;
        private InventorySlot _scrap;
        private SwordInventorySlot _swordInventorySlot;
        private CostByLevel? _weaponLevelCost;
        
        [SerializeField] private TextMeshProUGUI seedText;
        [SerializeField] private TextMeshProUGUI scrapText;

        //private CostByLevel? _slotsCost;

        public TextMeshProUGUI costWeaponText;
        public TextMeshProUGUI levelWeaponText;
        //public TextMeshProUGUI costSlotText;
        public UnityEvent onWeaponCanUpgrade;
        public UnityEvent onWeaponNotEnoughMoneyToUpgrade;
        public UnityEvent onWeaponNotMoreLevelsToUpgrade;
        public UnityEvent onWeaponToUpgraded;
        //public UnityEvent onSlotCanUpgrade;
        //public UnityEvent onSlotNotEnoughMoneyToUpgrade;
        // public UnityEvent onSlotNotMoreSlotsToUpgrade;
        // public UnityEvent onSlotToUpgraded;

        public void OnEnable()
        {
            UpdateData();
            
            var seed = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Seed);
            seedText.text = seed!=null ? $"{seed.amount}" : "0";
            var scrap = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Scrap);
            scrapText.text = scrap!=null ? $"{scrap.amount}" : "0";
        }


        public void UpdateData()
        {
            //TODO: até então está desligando o texto quando nao tem dinheiro. Mudar pra desligar so quando nao tem mais nivel
            _swordInventorySlot = InventoryManager.Instance.equippedItems.swordInventorySlot;
            levelWeaponText.text = $"Weapon level: {_swordInventorySlot.level}";
            _weaponLevelCost = _swordInventorySlot.level < BlacksmithConfigData.LevelsCost.Length
                ? BlacksmithConfigData.LevelsCost[_swordInventorySlot.level]
                : null;   
            // _slotsCost = _swordInventorySlot.slotsAmount < BlacksmithConfigData.SlotsCost.Length
            //     ? BlacksmithConfigData.SlotsCost[_swordInventorySlot.slotsAmount]
            //     : null;
            
            _seed = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Seed);
            _scrap = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Scrap);
            if (CanUpgradeWeapon())
            {
                costWeaponText.text = $"Seed: {_weaponLevelCost?.SeedCost} - Scrap: {_weaponLevelCost?.ScrapCost}";
                onWeaponCanUpgrade?.Invoke();
            }
            else
            {
                if (_weaponLevelCost == null)
                {
                    costWeaponText.text = "Level Max";
                    onWeaponNotMoreLevelsToUpgrade?.Invoke();
                }
                else
                {
                    costWeaponText.text = $"Level: Seed: {_weaponLevelCost?.SeedCost} - Scrap: {_weaponLevelCost?.ScrapCost}";
                    onWeaponNotEnoughMoneyToUpgrade?.Invoke();
                }
            }

            // if (CanUpgradeSlots())
            // {
            //     costSlotText.text = $"Slots: Seed: {_slotsCost?.SeedCost} - Scrap: {_slotsCost?.ScrapCost}";
            //     onSlotCanUpgrade?.Invoke();
            // }
            // else
            // {
            //     if (_slotsCost == null)
            //     {
            //         costSlotText.text = "Level Max";
            //         onSlotNotEnoughMoneyToUpgrade?.Invoke();
            //     }
            //     else
            //     {
            //         costSlotText.text = $"Slots: Seed: {_slotsCost?.SeedCost} - Scrap: {_slotsCost?.ScrapCost}";
            //         onSlotNotEnoughMoneyToUpgrade?.Invoke();
            //     }
            // }
        }

        [ContextMenu("Upgrade weapon")]
        public void UpgradeWeapon()
        {
            if (CanUpgradeWeapon())
            {
                onWeaponToUpgraded?.Invoke();
                _seed.RemoveAmount((int)_weaponLevelCost?.SeedCost);
                _scrap.RemoveAmount((int)_weaponLevelCost?.ScrapCost);
                _swordInventorySlot.level++;
                UpdateData();
            }
        }

        [ContextMenu("Upgrade Slots")]
        // public void UpgradeSlots()
        // {
        //     if (CanUpgradeSlots())
        //     {
        //         onSlotToUpgraded?.Invoke();
        //         _seed.RemoveAmount((int)_slotsCost?.SeedCost);
        //         _scrap.RemoveAmount((int)_slotsCost?.ScrapCost);
        //         _swordInventorySlot.slotsAmount++;
        //         UpdateData();
        //     }
        // }

        private bool CanUpgradeWeapon()
        {
            var hasEnoughSeed =  _seed != null && _weaponLevelCost != null && _seed.amount >= _weaponLevelCost?.SeedCost;
            var hasEnoughScrap = _scrap != null && _weaponLevelCost != null && _scrap.amount >= _weaponLevelCost?.ScrapCost;
            return hasEnoughScrap && hasEnoughSeed;
        }
        
        // private bool CanUpgradeSlots()
        // {
        //     var hasEnoughSeed = _seed != null && _slotsCost != null && _seed.amount >= _slotsCost?.SeedCost;
        //     var hasEnoughScrap = _scrap != null && _slotsCost != null && _scrap.amount >= _slotsCost?.ScrapCost;
        //     return hasEnoughScrap && hasEnoughSeed;
        // }

        public void Close()
        {
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }
    }
}
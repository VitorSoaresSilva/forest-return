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
        private CostByLevel? _slotsCost;

        public TextMeshProUGUI costWeaponText;
        public TextMeshProUGUI costSlotText;
        public UnityEvent OnWeaponCanUpgrade;
        public UnityEvent OnWeaponCannotUpgrade;
        public UnityEvent OnSlotCanUpgrade;
        public UnityEvent OnSlotCannotUpgrade;
        public void OnEnable()
        {
            UpdateData();
        }


        public void UpdateData()
        {
            //TODO: até então está desligando o texto quando nao tem dinheiro. Mudar pra desligar so quando nao tem mais nivel
            _swordInventorySlot = InventoryManager.Instance.equippedItems.swordInventorySlot;
            _weaponLevelCost = _swordInventorySlot.slotsAmount < BlacksmithConfigData.LevelsCost.Length
                ? BlacksmithConfigData.LevelsCost[_swordInventorySlot.level]
                : null;   
            _slotsCost = _swordInventorySlot.slotsAmount < BlacksmithConfigData.SlotsCost.Length
                ? BlacksmithConfigData.SlotsCost[_swordInventorySlot.level]
                : null;   
            _seed = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Seed);
            _scrap = InventoryManager.Instance.inventory.FindCurrencyByType(CurrencyType.Scrap);
            if (CanUpgradeWeapon())
            {
                costWeaponText.text = $"Seed: {_weaponLevelCost?.SeedCost} - Scrap: {_weaponLevelCost?.ScrapCost}";
                OnWeaponCanUpgrade?.Invoke();
            }
            else
            {
                costWeaponText.text = String.Empty;
                OnWeaponCannotUpgrade?.Invoke();
            }

            if (CanUpgradeSlots())
            {
                costSlotText.text = $"Seed: {_slotsCost?.SeedCost} - Scrap: {_slotsCost?.ScrapCost}";
                OnSlotCanUpgrade?.Invoke();
            }
            else
            {
                costSlotText.text = String.Empty;
                OnSlotCannotUpgrade?.Invoke();
            }
        }

        [ContextMenu("Upgrade weapon")]
        public void UpgradeWeapon()
        {
            if (CanUpgradeWeapon())
            {
                _seed.RemoveAmount((int)_weaponLevelCost?.SeedCost);
                _scrap.RemoveAmount((int)_weaponLevelCost?.ScrapCost);
                _swordInventorySlot.slotsAmount++;
                UpdateData();
            }
        }

        [ContextMenu("Upgrade Slots")]
        public void UpgradeSlots()
        {
            if (CanUpgradeSlots())
            {
                _seed.RemoveAmount((int)_slotsCost?.SeedCost);
                _scrap.RemoveAmount((int)_slotsCost?.ScrapCost);
                _swordInventorySlot.level++;
                UpdateData();
            }
        }

        private bool CanUpgradeWeapon()
        {
            var hasEnoughSeed =  _seed != null && _weaponLevelCost != null && _seed.amount >= _weaponLevelCost?.SeedCost;
            var hasEnoughScrap = _scrap != null && _weaponLevelCost != null && _scrap.amount >= _weaponLevelCost?.ScrapCost;
            return hasEnoughScrap && hasEnoughSeed;
        }
        private bool CanUpgradeSlots()
        {
            var hasEnoughSeed = _seed != null && _slotsCost != null && _seed.amount >= _slotsCost?.SeedCost;
            var hasEnoughScrap = _scrap != null && _slotsCost != null && _scrap.amount >= _slotsCost?.ScrapCost;
            return hasEnoughScrap && hasEnoughSeed;
        }

        public void Close()
        {
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }
    }
}
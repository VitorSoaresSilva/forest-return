using System;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class CraftsmanStore : MonoBehaviour
    {
        public GameObject maskLife, maskMana;
        public Button lifeButton, manaButton,unEquipMasksButton;
        public TriggerObject lifeMaskTrigger, manaMaskTrigger;

        private void OnEnable()
        {
            // lifeButton.onClick.AddListener(EquipLifeMask);
            // manaButton.onClick.AddListener(EquipManaMask);
            // unEquipMasksButton.onClick.AddListener(RemoveMasks);
        }

        public void EquipLifeMask()
        {
            InventoryManager.Instance.triggerInventory.RemoveTrigger(manaMaskTrigger);
            InventoryManager.Instance.triggerInventory.AddTrigger(lifeMaskTrigger);
            UpdatePlayerMask();
        }
        public void EquipManaMask()
        {
            InventoryManager.Instance.triggerInventory.RemoveTrigger(lifeMaskTrigger);
            InventoryManager.Instance.triggerInventory.AddTrigger(manaMaskTrigger);
            UpdatePlayerMask();
        }

        public void RemoveMasks()
        {
            InventoryManager.Instance.triggerInventory.RemoveTrigger(lifeMaskTrigger);
            InventoryManager.Instance.triggerInventory.RemoveTrigger(manaMaskTrigger);
            UpdatePlayerMask();
        }

        private void UpdatePlayerMask()
        {
            LevelManager.Instance.PlayerScript.UpdateMask();
        }


        public void Close()
        {
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }
        
    }
}

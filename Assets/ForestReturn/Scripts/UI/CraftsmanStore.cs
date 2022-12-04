using System;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class CraftsmanStore : MonoBehaviour
    {
        public TextMeshProUGUI descriptionMask;
        public GameObject maskLife, maskMana;
        public Button lifeButton, manaButton,unEquipMasksButton;
        public TriggerObject lifeMaskTrigger, manaMaskTrigger;

        public string manaMaskDescription, lifeMaskDescription;

        private void OnEnable()
        {
            if (InventoryManager.InstanceExists)
            {
                if (InventoryManager.Instance.triggerInventory.Contains(lifeMaskTrigger))
                {
                    maskLife.SetActive(true);
                    descriptionMask.text = lifeMaskDescription;
                }else if (InventoryManager.Instance.triggerInventory.Contains(manaMaskTrigger))
                {
                    maskMana.SetActive(true);
                    descriptionMask.text = manaMaskDescription;
                }
            }
        }

        public void EquipLifeMask()
        {
            InventoryManager.Instance.triggerInventory.RemoveTrigger(manaMaskTrigger);
            InventoryManager.Instance.triggerInventory.AddTrigger(lifeMaskTrigger);
            maskLife.SetActive(true);
            maskMana.SetActive(false);
            descriptionMask.text = lifeMaskDescription;
            UpdatePlayerMask();
        }
        public void EquipManaMask()
        {
            InventoryManager.Instance.triggerInventory.RemoveTrigger(lifeMaskTrigger);
            InventoryManager.Instance.triggerInventory.AddTrigger(manaMaskTrigger);
            maskMana.SetActive(true);
            maskLife.SetActive(false);
            descriptionMask.text = manaMaskDescription;
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

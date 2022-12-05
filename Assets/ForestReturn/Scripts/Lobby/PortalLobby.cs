using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Utilities;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Interactable
{
    public class PortalLobby : MonoBehaviour, IInteractable
    {
        public TriggerObject Lv1Complete;
        public TriggerObject Lv2Complete;
        // public TriggerObject Lv3Complete;
        public TriggerObject dialogTree1;
        public TriggerObject dialogTree2;
        public TriggerObject dialogTree3;
        public UnityEvent SetAsInteractable;
        public UnityEvent SetAsInteractableCantUserPortal;
        public UnityEvent SetAsNotInteractable;
        public UnityEvent SetAsNotInteractableCantUserPortal;


        
        
        
        [ContextMenu("Interact")]
        public void Interact()
        {
            if (!CanUsePortal()) return;
            if (GameManager.InstanceExists && InventoryManager.InstanceExists)
            {
                if (InventoryManager.Instance.triggerInventory.Contains(Lv2Complete))
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level03);
                }
                else if (InventoryManager.Instance.triggerInventory.Contains(Lv1Complete))
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level02);
                }
                else
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level01);
                }
            }
        }

        public void SetStatusInteract(bool status)
        {
            if (!CanUsePortal())
            {
                if (status)
                {
                    SetAsInteractableCantUserPortal.Invoke();
                }
                else
                {
                    SetAsNotInteractableCantUserPortal.Invoke();
                }
            }
            else
            {
                if (status)
                {
                    SetAsInteractable.Invoke();
                }
                else
                {
                    SetAsNotInteractable.Invoke();
                }
            }
        }

        private bool CanUsePortal()
        {
            if (InventoryManager.Instance.triggerInventory.Contains(Lv2Complete) && InventoryManager.Instance.triggerInventory.Contains(dialogTree3))
            {
                return true;
            }
            if (InventoryManager.Instance.triggerInventory.Contains(Lv1Complete) && InventoryManager.Instance.triggerInventory.Contains(dialogTree2))
            {
                return true;
            }
            if(InventoryManager.Instance.triggerInventory.Contains(dialogTree1))
            {
                return true;
            }
            return false;
            
        }
    }
}
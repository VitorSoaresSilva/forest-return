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
        public UnityEvent SetAsInteractable;
        public UnityEvent SetAsNotInteractable;
        
        [ContextMenu("Interact")]
        public void Interact()
        {
            Debug.Log("Interact");
            /*
             * if Level 01 started and not finished
             */
            if (GameManager.InstanceExists && InventoryManager.InstanceExists)
            {
                if (InventoryManager.Instance.triggerInventory.Contains(Lv1Complete))
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level02);
                }
                else if(InventoryManager.Instance.triggerInventory.Contains(Lv2Complete))
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level03);
                }
                else
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level01);
                }
            }
        }

        public void SetStatusInteract(bool status)
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
}
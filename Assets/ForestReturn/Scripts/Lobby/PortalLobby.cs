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
        public void Interact()
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.triggerInventory.Contains(Lv1Complete))
                {
                    GameManager.Instance.ChangeScene(Enums.Scenes.Level02);
                }
                else if(GameManager.Instance.triggerInventory.Contains(Lv2Complete))
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
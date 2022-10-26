using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Interactable
{
    public class PortalLobby : MonoBehaviour, IInteractable
    {
        public Enums.Scenes SceneToTeleport;
        public UnityEvent SetAsInteractable;
        public UnityEvent SetAsNotInteractable;
        public void Interact()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ChangeScene(SceneToTeleport);
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

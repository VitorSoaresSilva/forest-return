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
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeScene(SceneToTeleport);
            }
        }

        public void SetStatusInteract(bool status)
        {
            if (status)
            {
                Debug.Log("Set as Interactable");
                SetAsInteractable.Invoke();
            }
            else
            {
                Debug.Log("Set as not Interactable");
                SetAsNotInteractable.Invoke();
            }
        }
    }
}

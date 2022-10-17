using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Interactable
{
    public class PortalLobby : MonoBehaviour, IInteractable
    {
        public Enums.Scenes SceneToTeleport;
        public void Interact()
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeScene(SceneToTeleport);
            }
        }

        public void SetStatusInteract(bool status)
        {
            Debug.Log("Set as Interactable");
        }
    }
}

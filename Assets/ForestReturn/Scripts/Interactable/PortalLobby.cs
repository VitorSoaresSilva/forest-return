using _Developers.Vitor.Scripts.Interactable;
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
            GameManager.instance.ChangeScene(SceneToTeleport);
        }
    }
}

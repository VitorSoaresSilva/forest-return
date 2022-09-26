using ForestReturn.Scripts.PlayerAction.Managers;
using ForestReturn.Scripts.PlayerAction.Utilities;
using Interactable;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Interactable
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

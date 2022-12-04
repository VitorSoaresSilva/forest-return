using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class CraftsmanCollider : MonoBehaviour, IInteractable
    {
        public GameObject alert;
        private object _playerInput;

        public void Interact()
        {
            UiManager.Instance.OpenCraftsman();
            GameManager.Instance.PauseGame();
        }

        public void SetStatusInteract(bool status)
        {
            if (alert != null)
            {
                alert.SetActive(status);
            }
        }
    }
}

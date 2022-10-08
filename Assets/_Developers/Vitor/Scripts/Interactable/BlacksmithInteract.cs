using _Developers.Vitor.Scripts.Managers;
using _Developers.Vitor.Scripts.UI;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Interactable
{
    public class BlacksmithInteract : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            // if (GameManager.instance != null && UiManager.instance != null)
            // {
            //     UiManager.instance.ShowBlacksmith();
            // }
        }

        private void OnTriggerExit(Collider other)
        {
            UiManager.instance.HideBlacksmith();
        }
    }
}

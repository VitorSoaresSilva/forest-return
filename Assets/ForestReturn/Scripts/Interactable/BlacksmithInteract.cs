using Managers;
using UI;
using UnityEngine;

namespace Interactable
{
    public class BlacksmithInteract : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            if (GameManager.instance != null && UiManager.instance != null)
            {
                UiManager.instance.ShowBlacksmith();
            }
        }
    }
}

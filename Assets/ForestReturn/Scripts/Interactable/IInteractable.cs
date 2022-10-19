using UnityEngine.Events;

namespace ForestReturn.Scripts.Interactable
{
    public interface IInteractable
    {
        public void Interact();
        public void SetStatusInteract(bool status);
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Interactable
{
    public interface IInteractable
    {
        [ContextMenu("Interact")]
        public void Interact();
        public void SetStatusInteract(bool status);
    }
}
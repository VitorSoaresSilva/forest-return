using UnityEngine;

namespace Interactable
{
    public class ComputerDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private Door door;
        public void Interact()
        {
            // door.OpenWithKey()
        }
    }
}
using System;
using UnityEngine;

namespace Interactable
{
    public class PainelOpenDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private Door _door;
        [SerializeField] private GameObject panelInteract;

        public void Interact()
        {
            _door.Open();
        }

        private void OnTriggerEnter(Collider other)
        {
            panelInteract.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            panelInteract.SetActive(false);
        }
    }
}
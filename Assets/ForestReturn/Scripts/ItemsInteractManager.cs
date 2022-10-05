using System;
using Interactable;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class ItemsInteractManager : MonoBehaviour
    {
        public GameObject closest;
        public IInteractable closesScript;
        private void OnTriggerStay(Collider other)
        {
            // if (Vector3.Distance(other.transform.position, transform.position) <
            //     Vector3.Distance(closest.transform.position, transform.position))
            // {
            //     other.TryGetComponent(out closesScript);
            //     other.se
            // }
            // else
            // {
            //     
            // }
        }
    }
}
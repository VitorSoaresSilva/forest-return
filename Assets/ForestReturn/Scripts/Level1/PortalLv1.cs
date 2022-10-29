using System;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Utilities;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Interactable
{
    public class PortalLv1 : MonoBehaviour, IInteractable
    {
        public UnityEvent SetAsInteractable;
        public UnityEvent SetAsNotInteractable;

        public void Interact()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.ChangeScene(Enums.Scenes.Lobby);
            }
        }

        public void SetStatusInteract(bool status)
        {
            if (status)
            {
                SetAsInteractable.Invoke();
            }
            else
            {
                SetAsNotInteractable.Invoke();
            }
        }
    }
}

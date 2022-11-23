using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Interactable;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class PlayerInteractableHandler : MonoBehaviour
    {
        private List<ObjectInteractable> interactables = new ();
        public bool isActive;
        public ObjectInteractable CurrentInteractable { get; private set; }

        private void Start()
        {
            isActive = true;
            interactables = new List<ObjectInteractable>();
            CurrentInteractable = null;
            StartCoroutine(InteractUpdate());
        }

        private IEnumerator InteractUpdate()
        {
            while (isActive)
            {
                ObjectInteractable closestInteractable = null;
                if (interactables.Count == 1)
                {
                    closestInteractable = interactables[0];
                    if (CurrentInteractable != closestInteractable)
                    {
                        closestInteractable = interactables[0];
                    }
                }
                else if(interactables.Count > 1)
                {
                    closestInteractable = interactables[0];
                    var closestDistance = Vector3.Distance(transform.position, closestInteractable.ObjectTransform.position);
                    for (int i = 1; i < interactables.Count; i++)
                    {
                        var distance = Vector3.Distance(interactables[i].ObjectTransform.position, transform.position);
                        if ( distance < closestDistance)
                        {
                            closestInteractable = interactables[i];
                            closestDistance = distance;
                        }
                    }
                }

                if (closestInteractable != CurrentInteractable)
                {
                    CurrentInteractable?.Interactable.SetStatusInteract(false);
                    CurrentInteractable = closestInteractable;
                    CurrentInteractable.Interactable.SetStatusInteract(true);
                }
                
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger enter");
            if (!other.transform.root.TryGetComponent(out IInteractable interactable)) return;
            ObjectInteractable objectInteractable = new ObjectInteractable(other.transform.root,interactable);
            if (!interactables.Contains(objectInteractable))
            {
                interactables.Add(objectInteractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.transform.root.TryGetComponent(out IInteractable interactable)) return;
            ObjectInteractable objectInteractable = new ObjectInteractable(other.transform.root,interactable);
            var index = interactables.FindIndex(a => a.Interactable == interactable);
            if (index != -1)
            {
                interactables.RemoveAt(index);
                if (CurrentInteractable != null && objectInteractable.Interactable == CurrentInteractable.Interactable)
                {
                    CurrentInteractable = null;
                    interactable.SetStatusInteract(false);
                }
            }
        }

        public void Reset()
        {
            interactables.Remove(CurrentInteractable);
            CurrentInteractable = null;
        }
    }
    public class  ObjectInteractable
    {
        public readonly Transform ObjectTransform;
        public readonly IInteractable Interactable;

        public ObjectInteractable(Transform objectTransform, IInteractable interactable)
        {
            ObjectTransform = objectTransform;
            Interactable = interactable;
        }
    }
}
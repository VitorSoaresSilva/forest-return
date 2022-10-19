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
            other.transform.root.TryGetComponent(out IInteractable interactable);
            ObjectInteractable objectInteractable = new ObjectInteractable(other.transform,interactable);
            if (interactable != null && !interactables.Contains(objectInteractable))
            {
                interactables.Add(objectInteractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("exit");
            other.transform.root.TryGetComponent(out IInteractable interactable);
            ObjectInteractable objectInteractable = new ObjectInteractable(transform,interactable);
            var index = interactables.FindIndex(a => a.Interactable == interactable);
            if (interactable != null &&  index != -1)
            {
                Debug.Log("exit 2");
                interactables.RemoveAt(index);
                if (objectInteractable.Interactable != null && objectInteractable.Interactable == CurrentInteractable.Interactable)
                {
                    Debug.Log("exit 3");
                    CurrentInteractable = null;
                    interactable.SetStatusInteract(false);
                }
            }
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
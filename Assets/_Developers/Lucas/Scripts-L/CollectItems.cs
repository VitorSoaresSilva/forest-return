using System;
using System.Collections;
using System.Collections.Generic;
using _Developers.Vitor.Scripts.Interactable;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    private float time;
    void OnEnable()
    {
        time = Time.time + 1f;
    }
    
    void Update()
    {
        if (time < Time.time)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.TryGetComponent(out IInteractable interactable);
        interactable?.Interact();
        if (interactable != null)
        {
            gameObject.SetActive(false);
        }

    }
    
}

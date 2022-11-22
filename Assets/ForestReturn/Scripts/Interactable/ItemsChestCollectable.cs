using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

public class ItemsChestCollectable : MonoBehaviour, IInteractable
{
    public InventorySlot[] chestObjects;
    public GameObject alert;
    public TriggerObject triggerObject;
    public bool isOpen;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        SetStatusInteract(false);
        if (InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(triggerObject))
        {
            isOpen = true;
            //animação aberto
            _animator.SetTrigger("Open");
            enabled = false;
        }
        else
        {
            isOpen = false;
        }
    }
    
    public void Interact()
    {
        if (InventoryManager.InstanceExists && !isOpen)
        {
            InventoryManager.Instance.triggerInventory.AddTrigger(triggerObject);
            foreach (var items in chestObjects)
            {
                InventoryManager.Instance.inventory.AddItem(items.item);
            }
            isOpen = true;
            //animação aberto
            _animator.SetTrigger("Open");
            SetStatusInteract(false);
            enabled = false;
        }
    }

    public void SetStatusInteract(bool status)
    {
        if (alert != null)
        {
            alert.SetActive(status && !isOpen);
        }
        
    }
}

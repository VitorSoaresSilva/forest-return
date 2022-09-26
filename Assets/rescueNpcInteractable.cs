using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Managers;
using ForestReturn.Scripts.PlayerAction.Triggers;
using Interactable;
using UnityEngine;

public class rescueNpcInteractable : MonoBehaviour, IInteractable
{
    public GameObject[] npc;
    public TriggerObject npcRescued;

    private void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.triggerInventory.Contains(npcRescued))
            {
                foreach (var a in npc)
                {
                    Destroy(a);
                } 
            }
        }
    }

    public void Interact()
    {
        foreach (var a in npc)
        {
            Destroy(a);
        }
        GameManager.instance.triggerInventory.AddTrigger(npcRescued);
    }
}

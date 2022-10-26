using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.NPCs;
using UnityEngine;

public class DespawnNpcs : MonoBehaviour
{
    public TriggerObject Lv1Complete;
    public GameObject portalToLobby;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.TryGetComponent(out IBaseNpc baseNpc);
        if (baseNpc != null)
        {
            Destroy(other.transform.root.gameObject);
            GameManager.Instance.triggerInventory.AddTrigger(Lv1Complete);
           if (!portalToLobby.activeSelf)
            {
                portalToLobby.SetActive(true);
            }
        }
    }
}

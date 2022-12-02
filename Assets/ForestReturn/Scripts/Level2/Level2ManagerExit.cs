using System;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.NPCs;
using UnityEngine;

public class Level2ManagerExit : MonoBehaviour
{
    public TriggerObject KeyLevel2;
    public GameObject portalToLobby;

    void OnTriggerEnter(Collider collider)
    {
        bool hasKey = InventoryManager.Instance.triggerInventory.Contains(KeyLevel2);
        portalToLobby.SetActive(hasKey);
    }
}

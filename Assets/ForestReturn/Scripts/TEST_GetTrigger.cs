using System;
using ForestReturn.Scripts.PlayerAction.Managers;
using ForestReturn.Scripts.PlayerAction.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    public class TEST_GetTrigger : MonoBehaviour
    {
        public TriggerObject npcSavedTrigger;

        private void OnTriggerEnter(Collider other)
        {
            GameManager.instance.triggerInventory.AddTrigger(npcSavedTrigger);
            Destroy(gameObject);
        }
    }
}
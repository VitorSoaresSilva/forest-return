using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts
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
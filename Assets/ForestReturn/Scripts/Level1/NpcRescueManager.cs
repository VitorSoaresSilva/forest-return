using System;
using ForestReturn.Scripts.Enemies;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace ForestReturn.Scripts.Level1
{
    public class NpcRescueManager : MonoBehaviour
    {
        public TriggerObject npcRescued;
        public TriggerObject keyCage;
        private int _npcAmount = 3;
        private int _npcSavedAmount = 0;
        public GameObject[] npcGameObjects;
        public Action OnEnemyKilled;
        [SerializeField] private GameObject secondWaveTrigger;
        [SerializeField] private RoomEnemiesManager roomEnemiesManager;
        private void Start()
        {
            if (InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(npcRescued))
            {
                foreach (var npcGameObject in npcGameObjects)
                {
                    Destroy(npcGameObject);
                }
            }
            else
            {
                roomEnemiesManager.RoomClearedAction += EnemyKilled;
            }
        }

        private void OnDestroy()
        {
            roomEnemiesManager.RoomClearedAction -= EnemyKilled;
        }

        public void Rescue()
        {
            _npcSavedAmount++;
            if (_npcSavedAmount == _npcAmount && InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(keyCage))
            {
                InventoryManager.Instance.triggerInventory.AddTrigger(npcRescued);
                secondWaveTrigger.SetActive(true);
            }

        }

        private void EnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }
    }
}
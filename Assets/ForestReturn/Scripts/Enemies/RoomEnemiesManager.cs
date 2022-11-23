using System;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Enemies
{
    public class RoomEnemiesManager : MonoBehaviour
    {
        // public Door[] doors;
        public BaseEnemy[] enemies;
        public int enemiesAlive = 0;
        public TriggerObject roomCleared;
        public UnityEvent openDoorsEvent;
        public UnityEvent closeDoorsEvent;


        private void Awake()
        {
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            if (InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(roomCleared))
            {
                foreach (var enemy in enemies)
                {
                    Destroy(enemy.gameObject);
                }
                return;
            }
            
            enemiesAlive = enemies.Length;
            foreach (BaseEnemy baseEnemy in enemies)
            {
                baseEnemy.OnDead += BaseEnemyOnOnDead;
                baseEnemy.gameObject.SetActive(false);
            }
        }

        private void BaseEnemyOnOnDead()
        {
            if (--enemiesAlive <= 0)
            {
                RoomCleared();
            }
        }

        [ContextMenu("CloseDoors")]
        public void CloseDoors()
        {
            if (InventoryManager.InstanceExists && !InventoryManager.Instance.triggerInventory.Contains(roomCleared))
            {
                if (enemies.Length <= 0) return;
                
                closeDoorsEvent.Invoke();
                foreach (BaseEnemy baseEnemy in enemies)
                {
                    baseEnemy.gameObject.SetActive(true);
                }
            }
        }

        [ContextMenu("Open")]
        public void OpenDoors()
        {
            openDoorsEvent.Invoke();
        }

        private void RoomCleared()
        {
            if (InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.triggerInventory.AddTrigger(roomCleared);
            }
            OpenDoors();
        }
        
    }
}
using System;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using Unity.VisualScripting;
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
        
        private void Start()
        {
            if (GameManager.InstanceExists && GameManager.Instance.triggerInventory.Contains(roomCleared))
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
            if (GameManager.InstanceExists &&! GameManager.Instance.triggerInventory.Contains(roomCleared))
            {
                Debug.Log("Close Doors");
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
            Debug.Log("Open Doors");
            openDoorsEvent.Invoke();
        }

        private void RoomCleared()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.triggerInventory.AddTrigger(roomCleared);
            }
            OpenDoors();
        }
        
    }
}
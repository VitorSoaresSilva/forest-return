using System;
using System.Collections.Generic;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.Triggers
{
    [Serializable]
    [CreateAssetMenu(fileName = "newInventoryTrigger", menuName = "Trigger/Trigger Inventory")]
    public class TriggerInventoryObject : ScriptableObject
    {
        [field: SerializeField] public List<TriggerSlot> Triggers { get; private set; } = new();

        public void AddTrigger(TriggerObject triggerObject)
        {
            if (!Contains(triggerObject))
            {
                Triggers.Add(new TriggerSlot(triggerObject.id, triggerObject, new DateTime()));
            }
        }

        public void RemoveTrigger(TriggerObject triggerObject)
        {
            if (Contains(triggerObject))
            {
                var trigger = Triggers.Find(x => x.TriggerObject == triggerObject);
                Triggers.Remove(trigger);
            }
        }

        public bool Contains(TriggerObject triggerObject)
        {
            foreach (var trigger in Triggers)
            {
                if (trigger.TriggerObject == triggerObject || trigger.Id == triggerObject.id)
                {
                    return true;
                }
            }
            return false;
        }

        public void Init()
        {
            
        }
        public void Load()
        {
            foreach (var trigger in Triggers)
            {
                trigger.TriggerObject = InventoryManager.Instance.triggerDatabase.triggers[trigger.Id];
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            // path = string.Empty;
            Triggers.Clear();
        }
    }
}
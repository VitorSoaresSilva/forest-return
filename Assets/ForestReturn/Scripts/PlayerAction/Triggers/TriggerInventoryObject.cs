using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ForestReturn.Scripts.PlayerAction.Managers;

namespace ForestReturn.Scripts.PlayerAction.Triggers
{
    [CreateAssetMenu(fileName = "newInventoryTrigger", menuName = "Trigger/Trigger Inventory")]
    public class TriggerInventoryObject : ScriptableObject
    {
        [field: SerializeField] public List<TriggerSlot> Triggers { get; private set; } = new();
        public string path;

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

        [ContextMenu("Save")]
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, path));
            bf.Serialize(file, saveData);
            file.Close();
        }
        
        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, path)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, path), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
                foreach (var trigger in Triggers)
                {
                    trigger.TriggerObject = GameManager.instance.triggerDatabase.triggers[trigger.Id];
                }
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            path = string.Empty;
            Triggers.Clear();
        }
    }
}
using UnityEngine;

namespace ForestReturn.Scripts.Triggers
{
    [CreateAssetMenu(fileName = "newTriggerDatabase", menuName = "Trigger/Trigger Database")]
    public class TriggerDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public TriggerObject[] triggers;
        private void UpdateID()
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].id = i;
            }
        }
        public void OnAfterDeserialize()
        {
            UpdateID();
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
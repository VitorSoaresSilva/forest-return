using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Triggers
{
    [CreateAssetMenu(fileName = "newTrigger", menuName = "Trigger/Trigger")]
    public class TriggerObject : ScriptableObject
    {
        public int id;
        public string triggerName;
        public string description;
    }
}
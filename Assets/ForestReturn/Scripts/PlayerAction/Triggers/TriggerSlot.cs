using System;

namespace ForestReturn.Scripts.PlayerAction.Triggers
{
    [System.Serializable]
    public class TriggerSlot
    {
        public int Id;
        public TriggerObject TriggerObject;
        public DateTime TriggeredTime;

        public TriggerSlot(int id, TriggerObject triggerObject, DateTime triggeredTime)
        {
            Id = id;
            TriggerObject = triggerObject;
            TriggeredTime = triggeredTime;
        }
    }
}
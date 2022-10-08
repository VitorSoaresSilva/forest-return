using UnityEngine;

namespace ForestReturn.Scripts.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Item Database", order = 0)]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] items;

        [ContextMenu("Update ID's")]
        public void UpdateID()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].id = i;
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
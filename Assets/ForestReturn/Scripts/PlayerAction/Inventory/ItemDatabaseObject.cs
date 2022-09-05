using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Item Database", order = 0)]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] items;
        public Dictionary<int, ItemObject> GetItem = new ();

        public void OnBeforeSerialize()
        {
            GetItem = new Dictionary<int, ItemObject>();
        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].id = i;
                GetItem.Add(i,items[i]);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Item Database", order = 0)]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] items;
        public Dictionary<ItemObject, int> GetId = new ();
        public Dictionary<int, ItemObject> GetItem = new ();
        public void OnBeforeSerialize()
        {}

        public void OnAfterDeserialize()
        {
            GetId = new Dictionary<ItemObject, int>();
            GetItem = new Dictionary<int, ItemObject>();
            for (int i = 0; i < items.Length; i++)
            {
                GetId.Add(items[i], i);
                GetItem.Add(i,items[i]);
            }
        }
    }
}
using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public void Save()
        {
            InventoryManager.instance.Save();
        }
    }
}
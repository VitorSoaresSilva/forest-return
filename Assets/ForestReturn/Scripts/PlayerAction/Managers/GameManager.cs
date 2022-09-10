using System;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public void Save()
        {
            InventoryManager.instance.Save();
        }
    }
}
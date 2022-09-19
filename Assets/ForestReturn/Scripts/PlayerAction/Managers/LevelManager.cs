using UnityEngine;
using Utilities;
using Enums = ForestReturn.Scripts.PlayerAction.Utilities.Enums;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Enums.Scenes sceneIndex;
        public Vector3 pointToSpawn;
        public Player playerScript;
    }
}
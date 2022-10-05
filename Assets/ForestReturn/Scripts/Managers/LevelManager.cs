using UnityEngine;
using Utilities;
using Enums = ForestReturn.Scripts.PlayerAction.Utilities.Enums;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Enums.Scenes sceneIndex;
        public Vector3 pointToSpawn;
        public GameObject playerPrefab;
        public Player PlayerScript
        {
            get
            {
                if (playerScript != null) return playerScript;
                playerScript = FindObjectOfType<Player>();
                if (playerScript == null)
                {
                    var player = Instantiate(playerPrefab,pointToSpawn,Quaternion.identity);
                    playerScript = player.GetComponent<Player>();
                }
                return playerScript;
            }
        }
        public Player playerScript;
    }
}
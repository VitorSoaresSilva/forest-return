using System;
using ForestReturn.Scripts.PlayerAction.Triggers;
using ForestReturn.Scripts.PlayerAction.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class LobbyLevelManager : LevelManager
    {
        public TriggerObject npcSaved;
        public TriggerObject cutsceneWatched;
        
        private void Start()
        {
            if (GameManager.instance.triggerInventory.Contains(cutsceneWatched))
            {
                Debug.Log("Play cutscene");
            }
            else
            {
                Debug.Log("Do not play");
            }
            
            
            if (GameManager.instance.triggerInventory.Contains(npcSaved))
            {
                Debug.Log("Spawnar os npcs no cenario");
            }
            else
            {
                Debug.Log("Não Spawnar os npcs no cenario");
            }

            var teleportData = GameManager.instance.gameDataObject.TeleportData;
            if (teleportData.SceneStartIndex == sceneIndex && !teleportData.AlreadyReturned)
            {
                pointToSpawn = teleportData.PositionToSpawn;
            }
            playerScript.Init();
        }
    }
}
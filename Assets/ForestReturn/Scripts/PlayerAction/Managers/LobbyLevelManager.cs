using System;
using ForestReturn.Scripts.PlayerAction.Triggers;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Managers
{
    public class LobbyLevelManager : MonoBehaviour
    {
        public TriggerObject npcSaved;

        private void Start()
        {
            if (GameManager.instance.triggerInventory.Contains(npcSaved))
            {
                Debug.Log("Spawnar os npcs no cenario");
            }
            else
            {
                Debug.Log("Não Spawnar os npcs no cenario");
                
            }
        }
    }
}
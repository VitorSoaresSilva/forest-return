using System;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Teleport
{
    [Serializable]
    public struct TeleportData
    {
        public Vector3 teleportPointToSpawn;
        public Enums.Scenes teleportScene;

        public TeleportData(Vector3 teleportPointToSpawn, Enums.Scenes teleportScene)
        {
            this.teleportPointToSpawn = teleportPointToSpawn;
            this.teleportScene = teleportScene;
        }
    }
}
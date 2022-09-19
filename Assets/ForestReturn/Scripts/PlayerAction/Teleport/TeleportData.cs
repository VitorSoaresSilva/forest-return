using ForestReturn.Scripts.PlayerAction.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.Teleport
{
    public struct TeleportData
    {
        public Vector3 PositionToSpawn;
        public Enums.Scenes SceneStartIndex;
        public bool AlreadyReturned;

        public TeleportData(Vector3 positionToSpawn, Enums.Scenes scene, bool alreadyReturned = false)
        {
            PositionToSpawn = positionToSpawn;
            SceneStartIndex = scene;
            AlreadyReturned = alreadyReturned;
        }
    }
}
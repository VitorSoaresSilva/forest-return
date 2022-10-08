using ForestReturn.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.Teleport
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
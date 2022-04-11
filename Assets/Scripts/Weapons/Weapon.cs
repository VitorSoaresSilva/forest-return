using Artifacts;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class Weapon
    {
        public ArtifactsScriptableObject[] artifacts;
        public Weapon(int slotsAmount)
        {
            artifacts = new ArtifactsScriptableObject[slotsAmount];
        }
    }
}
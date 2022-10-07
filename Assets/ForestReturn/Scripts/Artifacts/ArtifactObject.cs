using ForestReturn.Scripts.Inventory;
using UnityEngine;

namespace ForestReturn.Scripts.Artifacts
{
    [CreateAssetMenu(fileName = "newArtifact", menuName = "Items/Artifacts")]
    public class ArtifactObject : ItemObject
    {
        public ArtifactObject[] incompatibleWith;

        private void Awake()
        {
            isUnique = true;
            isStackable = false;
            itemType = ItemType.Artifacts;
            hasLevels = false;
        }
    }
}
using Attributes;
using UnityEngine;

namespace Artifacts
{
    [CreateAssetMenu(fileName = "newArtifact", menuName = "ScriptableObject/Artifacts", order = 0)]
    public class ArtifactsScriptableObject : ScriptableObject
    {
        public string artifactName;
        public AttributeModifier[] modifiers;
        public GameObject model3d;
        public Sprite model2d;
    }
}
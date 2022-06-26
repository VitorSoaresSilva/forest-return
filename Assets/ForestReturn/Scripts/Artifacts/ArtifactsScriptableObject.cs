using System;
using Attributes;
using UnityEngine;

namespace Artifacts
{
    [Serializable]
    [CreateAssetMenu(fileName = "newArtifact", menuName = "ScriptableObject/Artifacts", order = 0)]
    public class ArtifactsScriptableObject : ScriptableObject
    {
        public string artifactName;
        public AttributeModifier[] modifiers;
        public GameObject model3d;
        public Texture model2d;

        public ArtifactsScriptableObject()
        {
            artifactName = "";
            modifiers = Array.Empty<AttributeModifier>();
            model3d = null;
            model2d = null;
        }
    }
}
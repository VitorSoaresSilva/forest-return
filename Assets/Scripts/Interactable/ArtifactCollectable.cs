using Artifacts;
using UnityEngine;
using Weapons;

namespace Interactable
{
    public class ArtifactCollectable : MonoBehaviour, IInteractable
    {
        public ArtifactsScriptableObject artifact;
        public void Interact()
        {
            if (WeaponManager.InstanceExists)
            {
                if (WeaponManager.Instance.weapon.AddArtifact(artifact))
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Out of space");
                }
            }
        }
    }
}
using Artifacts;
using Managers;
using Player;
using UnityEngine;

namespace Interactable
{
    public class ArtifactCollectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private ArtifactsScriptableObject artifactsScriptableObject;
        public void Interact()
        {
            if (GameManager.instance != null)
            {
                PlayerMain playerMain =  GameManager.instance.PlayerMain;
                if (playerMain != null)
                {
                    playerMain._weaponHolder.CollectArtifact(artifactsScriptableObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
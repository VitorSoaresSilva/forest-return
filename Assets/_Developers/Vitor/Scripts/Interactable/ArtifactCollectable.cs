using _Developers.Vitor.Scripts.Artifacts;
using _Developers.Vitor.Scripts.Managers;
using _Developers.Vitor.Scripts.Player;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Interactable
{
    public class ArtifactCollectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private ArtifactsScriptableObject artifactsScriptableObject;
        public void Interact()
        {
            // if (GameManager.instance != null)
            // {
            //     PlayerMain playerMain =  GameManager.instance.PlayerMain;
            //     if (playerMain != null)
            //     {
            //         playerMain._weaponHolder.CollectArtifact(artifactsScriptableObject);
            //         Destroy(gameObject);
            //     }
            // }
        }
    }
}
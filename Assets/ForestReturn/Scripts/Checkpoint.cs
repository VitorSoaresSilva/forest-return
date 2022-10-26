using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts
{
    public class Checkpoint : MonoBehaviour, IInteractable
    {
        public ParticleSystem[] _particleSystems;

        public void Interact()
        {
            foreach (var particle in _particleSystems)
            {
                particle.Play();
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Save();
            }
        }

        public void SetStatusInteract(bool status)
        {
        }
    }
}

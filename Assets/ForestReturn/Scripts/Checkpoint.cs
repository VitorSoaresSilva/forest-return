using _Developers.Vitor.Scripts.Interactable;
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
            if (GameManager.instance != null)
            {
                GameManager.instance.Save();
            }
        }
    }
}

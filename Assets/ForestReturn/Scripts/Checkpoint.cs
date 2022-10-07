using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts
{
    public class Checkpoint : MonoBehaviour
    {
        public ParticleSystem[] _particleSystems;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ForestReturn.Scripts.Player>(out var player))
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
}

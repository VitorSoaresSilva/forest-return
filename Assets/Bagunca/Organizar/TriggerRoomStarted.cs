using UnityEngine;

namespace Bagunca.Organizar
{
    public class TriggerRoomStarted : MonoBehaviour
    {
        [SerializeField] private TemporaryWaveManager _waveManager;

        private void OnTriggerEnter(Collider other)
        {
            _waveManager.StartRoom();
        }
    }
}

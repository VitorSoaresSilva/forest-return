using UnityEngine;

namespace Bagunca.Organizar
{
    public class enemyHit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("enemy hurt "+ other.name);
        }
    }
}

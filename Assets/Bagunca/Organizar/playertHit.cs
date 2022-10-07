using UnityEngine;

namespace Bagunca.Organizar
{
    public class playertHit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("player hit " + other.name);
        }
    }
}

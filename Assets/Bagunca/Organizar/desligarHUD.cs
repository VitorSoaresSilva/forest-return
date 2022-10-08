using _Developers.Vitor.Scripts.UI;
using UnityEngine;

namespace Bagunca.Organizar
{
    public class desligarHUD : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            UiManager.instance.hudPanel.SetActive(false);
        }

        public void LigarHUD()
        {
            UiManager.instance.hudPanel.SetActive(true);
        }
    }
}

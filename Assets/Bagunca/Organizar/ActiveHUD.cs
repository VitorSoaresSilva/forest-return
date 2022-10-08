using _Developers.Vitor.Scripts.UI;
using UnityEngine;

namespace Bagunca.Organizar
{
    public class ActiveHUD : MonoBehaviour
    {
        // Start is called before the first frame update
        public void startgame ()
        {
            UiManager.instance.EndAnimationStart();
        }
    }
}

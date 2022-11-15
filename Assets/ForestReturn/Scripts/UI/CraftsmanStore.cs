using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class CraftsmanStore : MonoBehaviour
    {
        public void Close()
        {
            UiManager.Instance.OpenCanvas(CanvasType.Hud);
        }
    }
}

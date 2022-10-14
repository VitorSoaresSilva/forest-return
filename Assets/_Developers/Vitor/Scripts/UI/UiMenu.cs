using _Developers.Vitor.Scripts.Managers;
using _Developers.Vitor.Scripts.Utilities;
using UnityEngine;

namespace _Developers.Vitor.Scripts.UI
{
    public class UiMenu : MonoBehaviour
    {
        /*public void OpenCanvas(CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Menu:
                    hud.SetActive(false);
                    break;
                case CanvasType.Hud:
                    hud.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
        public enum CanvasType
        {
            Options,
        }*/
        public void BackToLobby()
        {
            gameObject.SetActive(false);
            // GameManager.instance.LoadScene(Enums.Scenes.LobbySemCutscene);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
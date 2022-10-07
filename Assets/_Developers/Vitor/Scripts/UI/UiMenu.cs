using _Developers.Vitor.Scripts.Managers;
using _Developers.Vitor.Scripts.Utilities;
using UnityEngine;

namespace _Developers.Vitor.Scripts.UI
{
    public class UiMenu : MonoBehaviour
    {
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
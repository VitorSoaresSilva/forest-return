using Managers;
using UnityEngine;
using Utilities;

namespace UI
{
    public class UiMenu : MonoBehaviour
    {
        public void BackToLobby()
        {
            gameObject.SetActive(false);
            GameManager.instance.LoadScene(Enums.Scenes.LobbySemCutscene);
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
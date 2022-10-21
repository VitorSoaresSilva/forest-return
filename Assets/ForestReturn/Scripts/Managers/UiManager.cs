using System;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ForestReturn.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        private static readonly int HurtStringHash = Animator.StringToHash("Hurt");
        public GameObject hud;
        public Animator hurtAnimator;
        public GameObject menu;
        public GameObject pause;

        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (LevelManager.instance != null)
            {
                LevelManager.instance.PlayerScript.OnHurt += PlayerHurt;
            }

            OpenCanvas(CanvasType.Hud);
        }

        private void PlayerHurt()
        {
            hurtAnimator.SetTrigger(HurtStringHash);
        }


        public void OpenCanvas(CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Menu:
                    CloseAllMenu();
                    menu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case CanvasType.Hud:
                    CloseAllMenu();
                    hud.SetActive(true);
                    break;
                case CanvasType.Pause:
                    CloseAllMenu();
                    pause.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }

        private void CloseAllMenu()
        {
            hud.SetActive(false);
            menu.SetActive(false);
            pause.SetActive(false);
        }
    }

    public enum CanvasType
    {
        Menu,
        Hud,
        Pause,
    }
}
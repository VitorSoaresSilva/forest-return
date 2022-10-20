using System;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ForestReturn.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        [SerializeField] private Animator _hurtAnimator;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        //[SerializeField] private DisplayInventory displayInventory;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject pause;
        

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void PlayerHurt()
        {
            _hurtAnimator.SetTrigger(Hurt);
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

        public void CloseAllMenu()
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
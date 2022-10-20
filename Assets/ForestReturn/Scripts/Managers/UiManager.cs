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
        [SerializeField] private GameObject prefabCanvas;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        //[SerializeField] private DisplayInventory displayInventory;

        private void Start()
        {
            if (CanvasManager.instance == null)
            {
                Instantiate(prefabCanvas);
            }
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
                    CanvasManager.instance.menu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case CanvasType.Hud:
                    CloseAllMenu();
                    CanvasManager.instance.hud.SetActive(true);
                    break;
                case CanvasType.Pause:
                    CloseAllMenu();
                    CanvasManager.instance.pause.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }

        public void CloseAllMenu()
        {
            CanvasManager.instance.hud.SetActive(false);
            CanvasManager.instance.menu.SetActive(false);
            CanvasManager.instance.pause.SetActive(false);
        }
    }

    public enum CanvasType
    {
        Menu,
        Hud,
        Pause,
    }
}
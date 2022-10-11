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
                    hud.SetActive(false);
                    menu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    //displayInventory.gameObject.SetActive(true);
                    break;
                case CanvasType.Hud:
                    hud.SetActive(true);
                    menu.SetActive(false);
                    //displayInventory.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }

    public enum CanvasType
    {
        Menu,
        Hud,
    }
}
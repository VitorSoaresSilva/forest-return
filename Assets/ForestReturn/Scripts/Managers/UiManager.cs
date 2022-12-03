using System;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.UI;
using ForestReturn.Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace ForestReturn.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        
        public GameObject hud;
        public GameObject menu;
        public GameObject pause;
        public GameObject death;
        public GameObject blacksmith;
        public GameObject craftsman;
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (LevelManager.InstanceExists)
            {
                LevelManager.Instance.PlayerScript.OnDead += PlayerScriptOnOnDead;
            }
            
            OpenCanvas(CanvasType.Hud);
            if (GameManager.InstanceExists)
            {
                SetListeners();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ( LevelManager.InstanceExists)
            {
                LevelManager.Instance.PlayerScript.OnDead -= PlayerScriptOnOnDead;
            }
            
        }
        

        private void PlayerScriptOnOnDead()
        {
            Invoke(nameof(OpenDeathPanel),2);
        }

        private void OpenDeathPanel()
        {
            OpenCanvas(CanvasType.Death);
            GameManager.Instance.PauseGame();
        }

        private void SetListeners()
        {
            death.GetComponent<DeathCanvas>().SetListeners();
            pause.GetComponent<PauseCanvas>().SetListeners();
        }

        [ContextMenu("Open Blacksmith")]
        public void OpenBlacksmith()
        {
            OpenCanvas(CanvasType.Blacksmith);
        }
        
        public void OpenCraftsman()
        {
            OpenCanvas(CanvasType.Craftsman);
        }


        public void OpenCanvas(CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Menu:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    menu.SetActive(true);
                    // SetActionMap("Menu");
                    break;
                case CanvasType.Hud:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    hud.SetActive(true);
                    Cursor.lockState = CursorLockMode.Locked;
                    // SetActionMap("gameplay");
                    break;
                case CanvasType.Pause:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    pause.SetActive(true);
                    // SetActionMap("Menu");
                    break;
                case CanvasType.Death:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    death.SetActive(true);
                    // SetActionMap("Menu");
                    break;
                case CanvasType.Blacksmith:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    blacksmith.SetActive(true);
                    // SetActionMap("Menu");
                    break;
                case CanvasType.Craftsman:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    craftsman.SetActive(true);
                    // SetActionMap("Menu");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }

        // private void SetActionMap(string actionMap)
        // {
        //     LevelManager.Instance.PlayerScript..enabled = true;
        //     LevelManager.Instance.PlayerScript.playerInput.SwitchCurrentActionMap(actionMap);
        // }

        private void CloseAllMenu()
        {
            hud.SetActive(false);
            menu.SetActive(false);
            pause.SetActive(false);
            death.SetActive(false);
            blacksmith.SetActive(false);
            craftsman.SetActive(false);
        }
    }

    public enum CanvasType
    {
        Menu,
        Hud,
        Pause,
        Death,
        Blacksmith,
        Craftsman
    }
}
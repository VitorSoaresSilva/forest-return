using System;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace ForestReturn.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        private static readonly int HurtStringHash = Animator.StringToHash("Hurt");
        public GameObject hud;
        public Animator hurtAnimator;
        public GameObject menu;
        public GameObject pause;
        [SerializeField] private Button restartDeathButton;
        [SerializeField] private Button mainMenuDeathButton;
        [SerializeField] private Button quitDeathButton;
        
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuPauseButton;
        [SerializeField] private Button closePauseButton;
        [SerializeField] private Button quitPauseButton;
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (LevelManager.instance != null)
            {
                LevelManager.instance.PlayerScript.OnHurt += PlayerHurt;
            }
            OpenCanvas(CanvasType.Hud);
            SetListeners();
        }

        private void SetListeners()
        {
            mainMenuDeathButton.onClick.AddListener(() => {GameManager.instance.BackToMainMenu();});
            quitDeathButton.onClick.AddListener(() => {GameManager.instance.ExitGame();});
            
            resumeButton.onClick.AddListener(() => {GameManager.instance.ResumeGame();});
            mainMenuPauseButton.onClick.AddListener(() => {GameManager.instance.BackToMainMenu();});
            closePauseButton.onClick.AddListener(() => {GameManager.instance.ResumeGame();});
            quitPauseButton.onClick.AddListener(() => {GameManager.instance.ExitGame();});
            
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
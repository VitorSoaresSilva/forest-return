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
        
        [SerializeField] private Button restartDeathButton;
        [SerializeField] private Button mainMenuDeathButton;
        [SerializeField] private Button quitDeathButton;
        
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuPauseButton;
        [SerializeField] private Button closePauseButton;
        [SerializeField] private Button quitPauseButton;
        [SerializeField] private Button quitFerreiroButton;

        public GameObject prefabItemCollected;
        public GameObject itemCollectedParent;
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (LevelManager.InstanceExists)
            {
                LevelManager.Instance.PlayerScript.OnDead += PlayerScriptOnOnDead;
            }
            if(InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.inventory.OnItemCollected += InventoryOnItemCollected;
            }
            OpenCanvas(CanvasType.Hud);
            SetListeners();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ( LevelManager.InstanceExists)
            {
                LevelManager.Instance.PlayerScript.OnDead -= PlayerScriptOnOnDead;
            }
            if ( InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.inventory.OnItemCollected -= InventoryOnItemCollected;
            }
        }
        private void InventoryOnItemCollected(ItemCollectedData itemCollectedData)
        {
            if (prefabItemCollected == null || itemCollectedParent == null) return; // bug da unity
            var item = Instantiate(prefabItemCollected,itemCollectedParent.transform);
            if (itemCollectedParent != null)
            {
                item.transform.SetParent(itemCollectedParent.transform);
            }
            var itemCollectedAlert = item.GetComponent<ItemCollectedAlert>();
            itemCollectedAlert.SetText(itemCollectedData);
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
            
            restartDeathButton.onClick.AddListener(() => {GameManager.Instance.RestartFromCheckpoint();});
            mainMenuDeathButton.onClick.AddListener(() => {GameManager.Instance.BackToMainMenu();});
            quitDeathButton.onClick.AddListener(() => {GameManager.Instance.ExitGame();});
            
            resumeButton.onClick.AddListener(() => {GameManager.Instance.ResumeGame();});
            mainMenuPauseButton.onClick.AddListener(() => {GameManager.Instance.BackToMainMenu();});
            closePauseButton.onClick.AddListener(() => {GameManager.Instance.ResumeGame();});
            quitPauseButton.onClick.AddListener(() => {GameManager.Instance.ExitGame();});
            quitFerreiroButton.onClick.AddListener(() => {GameManager.Instance.ResumeGame();});
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
                    break;
                case CanvasType.Hud:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    hud.SetActive(true);
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case CanvasType.Pause:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    pause.SetActive(true);
                    break;
                case CanvasType.Death:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    death.SetActive(true);
                    break;
                case CanvasType.Blacksmith:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    blacksmith.SetActive(true);
                    break;
                case CanvasType.Craftsman:
                    CloseAllMenu();
                    Cursor.lockState = CursorLockMode.None;
                    craftsman.SetActive(true);
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
using System;
using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace ForestReturn.Scripts.UI
{
    public class MainMenu : Singleton<MainMenu>
    {
        public CardLoadGame[] cardsLoadGame;
        public Button playButton;
        public int currentActive = -1;

        public Button continueBtn;
        public Button loadGameBtn;

        private void Start()
        {
            if (GameManager.instance != null)
            {
                if (GameManager.instance.GameManagerInitFinished)
                {
                    UpdateAll();
                }
                else
                {
                    GameManager.instance.OnGameManagerInitFinished += UpdateAll;
                }
            }
        }

        private void UpdateUIMenu()
        {
            if (GameManager.instance == null) return;
            if (GameManager.instance.IndexSaveSlot != -1)
            {
                if (continueBtn.gameObject != null)
                {
                    // continueBtn.gameObject.SetActive(true);
                    continueBtn.enabled = true; 
                }
                loadGameBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Load Game";
            }
            else
            {
                if (continueBtn.gameObject != null)
                {
                    // continueBtn.gameObject.SetActive(true);
                    continueBtn.enabled = true; 
                }
                // continueBtn.gameObject.SetActive(false);
                loadGameBtn.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
                // continueBtn.enabled = false;
            }
        }

        private void UpdateCardsLoad()
        {
            if (GameManager.instance.IndexSaveSlot != -1)
            {
                playButton.enabled = true;
                currentActive = GameManager.instance.IndexSaveSlot;
                cardsLoadGame[currentActive].SetState(true);
            }
            for (int i = 0; i < 3; i++)
            {
                var a = GameManager.instance.savedGameDataTemporary[i];
                cardsLoadGame[i].Init(a.loadSuccess ? a.generalDataObject.LastSaveString : "New Game");
            }
        }
        public void SetLoadIndex(int index)
        {
            if (currentActive != -1)
            {
                cardsLoadGame[currentActive].SetState(false);
            }
            currentActive = index;
            cardsLoadGame[currentActive].SetState(true);
            playButton.enabled = true;
            GameManager.instance.SelectIndexSaveSlot(index);
        }

        public void DeleteSave()
        {
            GameManager.instance.DeleteSlotIndex();
            UpdateAll();
        }
        public void Play()
        {
            GameManager.instance.Play();
        }

        private void UpdateAll()
        {
            UpdateUIMenu();
            UpdateCardsLoad();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDisable()
        {
            GameManager.instance.OnGameManagerInitFinished -= UpdateAll;
        }
    }
}

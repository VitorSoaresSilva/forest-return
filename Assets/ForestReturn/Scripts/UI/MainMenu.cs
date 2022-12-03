using System;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace ForestReturn.Scripts.UI
{
    public class MainMenu : Singleton<MainMenu>
    {
        public CardLoadGame[] cardsLoadGame;
        // public Button playButton;
        private int _currentSlotIndexActive = -1;

        // public Button continueBtn;
        // public Button loadGameBtn;
        

        private void Start()
        {
            if (GameManager.InstanceExists)
            {
                if (GameManager.Instance.GameManagerInitFinished)
                {
                    UpdateAll();
                }
                else
                {
                    GameManager.Instance.OnGameManagerInitFinished += UpdateAll;
                }
            }
        }
        public void SetLoadIndex(int index)
        {
            if (_currentSlotIndexActive != -1)
            {
                cardsLoadGame[_currentSlotIndexActive].SetState(false);
            }
            _currentSlotIndexActive = index;
            cardsLoadGame[_currentSlotIndexActive].SetState(true);
            // playButton.enabled = true;
            GameManager.Instance.SelectIndexSaveSlot(index);
        }

        public void DeleteSave()
        {
            GameManager.Instance.DeleteSlotIndex();
            UpdateAll();
        }
        public void Play()
        {
            GameManager.Instance.Play();
        }

        public void QuitGame()
        {
            GameManager.Instance.ExitGame();
        }

        private void UpdateAll()
        {
            if (!GameManager.InstanceExists) return;
            for (int i = 0; i < 3; i++)
            {
                var saveGameData = GameManager.Instance.savedGameDataTemporary[i];
                cardsLoadGame[i].Init(saveGameData.loadSuccess ? saveGameData.generalDataObject.lastSaveString : "New Game", saveGameData.loadSuccess);
                cardsLoadGame[i].SetState(false);
            }
            if (GameManager.Instance.IndexSaveSlot < 0)
            {
                // loadGameBtn.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
                // continueBtn.gameObject.SetActive(false);
                _currentSlotIndexActive = 0;
                cardsLoadGame[_currentSlotIndexActive].SetState(true);
                GameManager.Instance.SelectIndexSaveSlot(_currentSlotIndexActive);
            }
            else
            { 
                // loadGameBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Load Game";
                // continueBtn.gameObject.SetActive(true);
                
                _currentSlotIndexActive = GameManager.Instance.IndexSaveSlot;
                cardsLoadGame[_currentSlotIndexActive].SetState(true);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDisable()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnGameManagerInitFinished -= UpdateAll;
            }
        }
    }
}

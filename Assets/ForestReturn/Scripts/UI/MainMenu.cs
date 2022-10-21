using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.Managers;
using TMPro;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class MainMenu : Singleton<MainMenu>
    {
        public CardLoadGame[] cardsLoadGame;
        public Button playButton;
        public int currentActive = -1;
        private void Start()
        {
            UpdateUIMenu();
            UpdateCardsLoad();
        }

        public Button continueBtn;
        public Button loadGameBtn;
        public void UpdateUIMenu()
        {
            if (GameManager.instance == null) return;
            if (GameManager.instance.IndexSaveSlot != -1)
            {
                continueBtn.gameObject.SetActive(true);
                loadGameBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Load Game";
                continueBtn.enabled = true;
            }
            else
            {
                continueBtn.gameObject.SetActive(false);
                loadGameBtn.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
                continueBtn.enabled = false;
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
                if (a.loadSuccess)
                {
                    cardsLoadGame[i].Init(a.generalDataObject.LastSaveString);
                }
                else
                {
                    cardsLoadGame[i].Init("New Game");
                }
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
        public void Play()
        {
            GameManager.instance.Play();
        }
    }
}

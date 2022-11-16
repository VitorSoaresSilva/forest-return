using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
namespace ForestReturn.Scripts.UI
{
    public class PauseCanvas : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuPauseButton;
        [SerializeField] private Button closePauseButton;
        [SerializeField] private Button quitPauseButton;


        public void SetListeners()
        {
            resumeButton.onClick.AddListener(() => {GameManager.Instance.ResumeGame();});
            mainMenuPauseButton.onClick.AddListener(() => {GameManager.Instance.BackToMainMenu();});
            closePauseButton.onClick.AddListener(() => {GameManager.Instance.ResumeGame();});
            quitPauseButton.onClick.AddListener(() => {GameManager.Instance.ExitGame();});
        }
    }
}
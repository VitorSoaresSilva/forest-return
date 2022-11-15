using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class DeathCanvas : MonoBehaviour
    {
        [SerializeField] private Button restartDeathButton;
        [SerializeField] private Button mainMenuDeathButton;
        [SerializeField] private Button quitDeathButton;

        public void SetListeners()
        {
            restartDeathButton.onClick.AddListener(() => {GameManager.Instance.RestartFromCheckpoint();});
            mainMenuDeathButton.onClick.AddListener(() => {GameManager.Instance.BackToMainMenu();});
            quitDeathButton.onClick.AddListener(() => {GameManager.Instance.ExitGame();});
        }
    }
}
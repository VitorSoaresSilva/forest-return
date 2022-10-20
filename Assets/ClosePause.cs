using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ClosePause : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        resumeButton.onClick.AddListener(() => {GameManager.instance.ResumeGame();});
        mainMenuButton.onClick.AddListener(() => {GameManager.instance.MainMenu();});
        closeButton.onClick.AddListener(() => {GameManager.instance.ResumeGame();});
        quitButton.onClick.AddListener(() => {GameManager.instance.ExitGame();});
    }
    
}

using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        //TODO: RESTART GAME(DEATH)
        //restartButton.onClick.AddListener(() => {GameManager.instance.RestartGame();});
        mainMenuButton.onClick.AddListener(() => {GameManager.instance.MainMenu();});
        quitButton.onClick.AddListener(() => {GameManager.instance.ExitGame();});
    }
    
}
using Managers;
using UnityEngine;
using Utilities;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public GameObject hudPanel;
   
    //Carrega a cena da primeira fase
    public void Play()
    {
        GameManager.instance.LoadScene(Enums.Scenes.Lobby);
        hudPanel.SetActive(true);
    }

    //Carrega painel de configura��es
    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    //Fecha painel de configura��es
    public void CloseOptions()
    {
        OptionsPanel.SetActive(false);
    }

    //Carrega painel de cr�ditos
    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }

    //Fecha painel de cr�tidos
    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
    }

   
    //Fecha o jogo
    public void QuitGame()
    {
        Application.Quit();
    }
}

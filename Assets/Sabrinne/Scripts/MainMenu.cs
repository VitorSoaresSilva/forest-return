using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;

   
    //Carrega a cena da primeira fase
    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
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

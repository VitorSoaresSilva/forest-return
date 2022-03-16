using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private GameObject OptionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        OptionsPanel = GameObject.FindGameObjectWithTag("OptionsPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    //Carrega a cena da primeira fase
    public void PlayLevel1()
    {
        //SceneManager.LoadScene();
    }
}

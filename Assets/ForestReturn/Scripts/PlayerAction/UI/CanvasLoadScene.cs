using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ForestReturn.Scripts.PlayerAction.Managers;
using ForestReturn.Scripts.PlayerAction.UI;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLoadScene : MonoBehaviour
{
    public GameObject[] saveButtons;
    public CardLoadGame[] cardsLoadGame;
    public Button playButton;
    public int currentActive = -1;
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

    private void OnEnable()
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
            if (a.LoadSuccess)
            {
                cardsLoadGame[i].Init(a.generalDataObject.LastSaveString);
            }
            else
            {
                cardsLoadGame[i].Init("New Game");
            }
        }
        
        
        // //TODO: Mostrar para o usuario alguns dados para ele escolher o save
        // var availableSaves = GameManager.instance.GetAvailableSaves();
        // for (int i = 0; i < saveButtons.Length; i++)
        // {
        //     saveButtons[i].SetActive(true);
        //     // saveButtons[i].SetActive(availableSaves.Length <= i && availableSaves[i]);
        // }
    }
}

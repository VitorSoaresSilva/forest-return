using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction.Managers;
using UnityEngine;

public class CanvasLoadScene : MonoBehaviour
{
    public GameObject[] saveButtons;
    public void SetLoadIndex(int index)
    {
        GameManager.instance.SelectIndexSaveSlot(index);
    }

    private void OnEnable()
    {
        //TODO: Mostrar para o usuario alguns dados para ele escolher o save
        var availableSaves = GameManager.instance.GetAvailableSaves();
        for (int i = 0; i < saveButtons.Length; i++)
        {
            saveButtons[i].SetActive(true);
            // saveButtons[i].SetActive(availableSaves.Length <= i && availableSaves[i]);
        }
    }
}

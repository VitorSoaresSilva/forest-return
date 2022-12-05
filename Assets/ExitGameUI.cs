using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameUI : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.Instance.ExitGame();
    }
}

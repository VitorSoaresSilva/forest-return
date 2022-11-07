using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Managers;
using UnityEngine;

public class BlacksmithCollider : MonoBehaviour, IInteractable
{
    public GameObject alert;
    private object _playerInput;

    public void Interact()
    {
        UiManager.Instance.OpenBlacksmith();
        GameManager.Instance.PauseGame();
    }

    public void SetStatusInteract(bool status)
    {
        if (alert != null)
        {
            alert.SetActive(status);
        }
    }
}

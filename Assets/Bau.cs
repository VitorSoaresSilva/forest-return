using System;
using System.Collections;
using System.Collections.Generic;
using Interactable;
using UI;
using UnityEngine;
using Weapons;

public class Bau : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponsScriptableObject _weaponsScriptableObject;
    private Weapon _weapon;
    private void Start()
    {
        // _weapon = new Weapon();
    }

    public void Interact()
    {
        UiManager.instance.ShowItem(_weapon);
    }
    
}

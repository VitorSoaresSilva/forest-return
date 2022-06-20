using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class ItemPopUp : MonoBehaviour
{
    public GameObject root;
    public RawImage image;
    public TextMeshProUGUI title;
    public UIArtifactCard[] uiArtifacts;
    public void Show(Weapon weapon)
    {
        // if (root.activeSelf) return;
        // root.SetActive(true);
        // if (weapon.weaponConfig.image != null)
        // {
        //     image.texture = weapon.weaponConfig.image;
        // }
        // title.text = weapon.weaponConfig.weaponName;
        // for (int i = 0; i < weapon.artifacts.Length; i++)
        // {
        //     if (weapon.artifacts[i] != null)
        //     {
        //         uiArtifacts[i].ReceiveData(weapon.artifacts[i]);
        //     }
        // }
    }

    public void Hide()
    {
        // root.SetActive(false);
        // image.texture = null;
        // title.text = String.Empty;
        // foreach (var uiArtifact in uiArtifacts)
        // {
        //     uiArtifact.ResetValues();
        // }
    }
}

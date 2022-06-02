using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class ItemPopUp : MonoBehaviour
{
    public GameObject root;
    public RawImage image;
    public TextMeshProUGUI title;
    // public TextMeshProUGUI[] textDetails;
    public UIArtifact[] uiArtifacts;
    public void Show(Weapon weapon)
    {
        if (root.activeSelf) return;
        root.SetActive(true);
        if (weapon.weaponConfig.image != null)
        {
            image.texture = weapon.weaponConfig.image;
        }
        title.text = weapon.weaponConfig.weaponName;
        for (int i = 0; i < weapon.artifacts.Length; i++)
        {
            // textDetails[i].text = weapon.artifacts[i].artifactName;
            // textDetails[i].gameObject.SetActive(true);
            Debug.Log(weapon.artifacts[i].artifactName);
            uiArtifacts[i].SetValues(weapon.artifacts[i]);
        }

    }

    public void Hide()
    {
        root.SetActive(false);
        image.texture = null;
        title.text = String.Empty;
        // foreach (var textDetail in textDetails)
        // {
        //     textDetail.text = String.Empty;
        //     textDetail.enabled = false;
        // }

        foreach (var uiArtifact in uiArtifacts)
        {
            uiArtifact.ResetValues();
        }
    }
}

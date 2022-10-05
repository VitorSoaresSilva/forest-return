using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class desligarHUD : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UiManager.instance.hudPanel.SetActive(false);
    }

    public void LigarHUD()
    {
        UiManager.instance.hudPanel.SetActive(true);
    }
}

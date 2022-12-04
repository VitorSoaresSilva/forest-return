using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogScreen : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public void SetDialogText(string value)
    {
        textMeshProUGUI.text = value;
    }
}

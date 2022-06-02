using System;
using System.Collections;
using System.Collections.Generic;
using Artifacts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIArtifact : MonoBehaviour
{
    private GameObject _owner;
    public RawImage image;
    public TextMeshProUGUI titleName;
    public TextMeshProUGUI[] modifiersText;
    private void Start()
    {
        // _owner = GetComponent<GameObject>();
    }

    public void SetValues(ArtifactsScriptableObject artifact)
    {
        // _owner.SetActive(true);
        gameObject.SetActive(true);
        image.texture = artifact.model2d;
        titleName.text = artifact.artifactName;
        for (int i = 0; i < artifact.modifiers.Length; i++)
        {
            var text = $"{artifact.modifiers[i].value} {artifact.modifiers[i].type}";
            modifiersText[i].text = text;
            modifiersText[i].gameObject.SetActive(true);
        }
    }
    public void ResetValues()
    {
        _owner.SetActive(false);
        image.texture = null;
        titleName.text = string.Empty;
        foreach (var textMeshProUGUI in modifiersText)
        {
            textMeshProUGUI.text = string.Empty;
            textMeshProUGUI.gameObject.SetActive(false);
        }
    }
    
}

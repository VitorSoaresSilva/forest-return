using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forja : MonoBehaviour
{
    [SerializeField] private GameObject pointLight, fire;
    private MeshRenderer meshRenderer;

    public void ChangeState(bool state)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        var _materialsRef = meshRenderer.materials;
        pointLight.SetActive(state);
        fire.SetActive(state);
        if (state)
        {
            foreach (var materialRef in _materialsRef)
            {
                materialRef.EnableKeyword("_EMISSION");
            }
            
            
        }
        else
        {
            foreach (var materialRef in _materialsRef)
            {
                materialRef.DisableKeyword("_EMISSION");
            }
        }
    }
}

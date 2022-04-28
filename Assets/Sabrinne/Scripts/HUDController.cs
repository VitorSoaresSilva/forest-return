using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject InventarioPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventario()
    {
        InventarioPanel.SetActive(true);
    }

    public void CloseInventario()
    {
        InventarioPanel.SetActive(false);
    }
}

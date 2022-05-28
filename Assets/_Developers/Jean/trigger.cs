using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance ambient;
    public FMODUnity.EventReference fmodevent;

    



    private void Start()
    {
        gameObject.tag = "FloorCalm";

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FloorCalm"))
        {
            ambient = FMODUnity.RuntimeManager.CreateInstance("event:/ambient/standard");
           // ambient = FMODUnity.RuntimeManager.CreateInstance(fmodevent);
            ambient.start();
        }
    }
    void Update()
    {
            ambient.setParameterByName("situation", 0);
    }
}

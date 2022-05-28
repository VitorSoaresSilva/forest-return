using UnityEngine;
using FMOD;
using FMODUnity;
using FMODUnityResonance;

public class ParametersSetByName : MonoBehaviour
{
    FMOD.Studio.EventInstance standard;
    int Calm = 0;
    int Battle = 1;
   // int Win = 3;
  //  int Loss = 2;

    private void Start()
    {
        standard = FMODUnity.RuntimeManager.CreateInstance("event:/ambient/standard");
        standard.start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FloorCalm")
            standard.setParameterByName("calmo,perigo,morte,vitoria", Calm, false);

        if (other.tag == "FloorBattle")
            standard.setParameterByName("calmo,perigo,morte,vitoria", Battle, false);
        //else (other.tag == "")

    }

    // private void OnTriggerExit(Collider other)
    //  {
    //     if (other.tag == "FloorCalm")
    //         standard.setParameterByName("Ambience Fade", 0f);
    // }
}

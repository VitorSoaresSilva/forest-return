using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ActiveHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public void startgame ()
    {
        UiManager.instance.EndAnimationStart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introOne : MonoBehaviour
{
    [SerializeField] private Animator animator2;

    public void PlayIntro2()
    {
        animator2.SetTrigger("play");
    }
}

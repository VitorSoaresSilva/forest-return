using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActiveBoss : MonoBehaviour
{
    public BossScript bossScript;
    private void OnTriggerEnter(Collider other)
    {
        bossScript.StartBoss();
    }
}

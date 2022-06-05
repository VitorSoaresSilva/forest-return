using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player hit " + other.name);
    }
}

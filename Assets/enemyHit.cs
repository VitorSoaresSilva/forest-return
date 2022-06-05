using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy hurt "+ other.name);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class TestePlayer : BaseCharacter
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("batata");
    }
}

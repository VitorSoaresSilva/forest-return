using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Enemies;
using UnityEngine;

public class TriggerCloseDoor : MonoBehaviour
{
    public RoomEnemiesManager room;
    private void OnTriggerEnter(Collider other)
    {
        room.CloseDoors();
    }
}

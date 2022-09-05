using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TriggerRoomStarted : MonoBehaviour
{
    [SerializeField] private TemporaryWaveManager _waveManager;

    private void OnTriggerEnter(Collider other)
    {
        _waveManager.StartRoom();
    }
}

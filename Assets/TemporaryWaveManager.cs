using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class TemporaryWaveManager : MonoBehaviour
{
    public BaseCharacter[] enemies;
    [SerializeField] private int _amountOfEnemies;
    public Door[] doors;
    [SerializeField] private bool isRunning;

    private void Start()
    {
        _amountOfEnemies = enemies.Length;
        foreach (var enemy in enemies)
        {
            enemy.OnDead += HandleEnemyDead;
        }
    }

    private void HandleEnemyDead()
    {
        _amountOfEnemies--;
        if (_amountOfEnemies <= 0)
        {
            isRunning = false;
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }

    private void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.Close();
        }
    }

    public void StartRoom()
    {
        if (isRunning) return;
        isRunning = true;
        CloseDoors();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction;
using ForestReturn.Scripts.PlayerAction.Managers;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem[] _particleSystems;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ForestReturn.Scripts.Player>(out var player))
        {
            foreach (var particle in _particleSystems)
            {
                particle.Play();
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.Save();
            }
        }
    }
}

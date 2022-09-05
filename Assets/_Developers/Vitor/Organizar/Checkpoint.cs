using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.PlayerAction;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem[] _particleSystems;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ForestReturn.Scripts.PlayerAction.Player>(out var player))
        {
            foreach (var particle in _particleSystems)
            {
                particle.Play();
            }
            GameManager.instance.Save();
        }
    }
}

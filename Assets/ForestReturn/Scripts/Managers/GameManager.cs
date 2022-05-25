using System;
using Character;
using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public PlayerMovement Player { get; private set; }

        private void Start()
        {
            if (Player == null)
            {
                Player = FindObjectOfType<PlayerMovement>();
            }
        }
    }
}
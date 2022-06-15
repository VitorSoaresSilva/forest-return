using System;
using Character;
using Player;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public PlayerMain PlayerMain { get; private set; }

        private void Start()
        {
            // if (PlayerMain == null)
            // {
            //     PlayerMain = FindObjectOfType<PlayerMain>();
            // }
        }

        public PlayerMain GetPlayerScript()
        {
            if (PlayerMain == null)
            {
                PlayerMain = FindObjectOfType<PlayerMain>();
            }
            return PlayerMain;
        }
    }
}
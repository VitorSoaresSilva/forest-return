using System;
using Character;
using Player;
using UnityEngine;
using Utilities;
using Weapons;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public PlayerMain PlayerMain { get; private set; }

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
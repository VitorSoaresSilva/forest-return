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
        private PlayerMain _playerMain;
        public PlayerMain PlayerMain
        {
            get
            {
                if (_playerMain == null)
                {
                    _playerMain = FindObjectOfType<PlayerMain>();
                }
                return _playerMain;
            }
        }
    }
}
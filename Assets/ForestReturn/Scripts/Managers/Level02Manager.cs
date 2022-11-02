using ForestReturn.Scripts.Teleport;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level02Manager : LevelManager
    {
        protected override void Start()
        {
            base.Start();
            PlayerScript.Init();
            if (UiManager.Instance != null)
            {
                UiManager.Instance.Init();
            }
        }
    }
}
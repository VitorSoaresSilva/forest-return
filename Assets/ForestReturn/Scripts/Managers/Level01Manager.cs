using ForestReturn.Scripts.Teleport;
using UnityEngine;

namespace ForestReturn.Scripts.Managers
{
    public class Level01Manager : LevelManager
    {
        public Vector3 pointToNpcGoAway;
        protected override void Start()
        {
            base.Start();
            PlayerScript.Init();
            if (UiManager.InstanceExists)
            {
                UiManager.Instance.Init();
            }
        }
    }
}
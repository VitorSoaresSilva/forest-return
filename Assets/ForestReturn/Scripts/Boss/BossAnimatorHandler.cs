using System;
using Unity.VisualScripting.FullSerializer.Internal.Converters;
using UnityEngine;

namespace ForestReturn.Scripts.Boss
{
    public class BossAnimatorHandler : MonoBehaviour
    {
        private BossScript _bossScript;

        private void Start()
        {
            _bossScript = GetComponentInParent<BossScript>();
        }

        public void HandleStartWaitingAttack()
        {
            _bossScript.HandleBossWaitingAttack();
        }

        public void HandleAnimationDeadEnded()
        {
            _bossScript.HandleAnimationDeadEnded();
        }
    }
}
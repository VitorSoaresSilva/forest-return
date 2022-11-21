using System;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class PlayerAttacker : MonoBehaviour
    {
        private AnimatorHandler _animatorHandler;
        public string lightAttackAnimationName = "LightAttack";
        

        private void Awake()
        {
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void HandleLightAttack()
        {
            Debug.Log("Attack");
            _animatorHandler.PlayerTargetAnimation(lightAttackAnimationName, true);
        }

        // public void HandleVinesAttack()
        // {
        //     _animatorHandler.PlayerTargetAnimation(vinesAttackAnimationName, true);
        // }
    }
}
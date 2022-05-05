using System;
using UnityEngine;
using UnityEngine.XR;

namespace Enemies.StateMachine
{
    public class StateMachine: MonoBehaviour
    {
        private BaseState currentState;

        private void Start()
        {
            ChangeState(new IdleState());
        }

        private void Update()
        {
            currentState?.UpdateState();
        }

        private void ChangeState(BaseState newState)
        {
            currentState?.DestroyState();
            currentState = newState;
            
            if (currentState == null) return;
            currentState.owner = this;
            currentState.PrepareState();
        }
    }
}
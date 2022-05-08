using System;
using Enemies.StateMachine;
using UnityEngine;
namespace Enemies
{
    public class EnemyStateController : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        
        private void Awake()
        {
            _stateMachine = GetComponent<EnemyStateMachine>();
        }

        private void StartAlertState()
        {
            // avisar outros inimigos
            // _stateMachine.cha
        }

        
    }
}
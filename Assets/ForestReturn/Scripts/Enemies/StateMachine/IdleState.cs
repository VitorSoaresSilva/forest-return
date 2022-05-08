using UnityEngine;

namespace Enemies.StateMachine
{
    public class IdleState: BaseState
    {
        public override void UpdateState()
        {
            Debug.Log("Idle");
        }

        public override void DestroyState()
        {
            
        }
        public override void PrepareState()
        {
            
        }
    }
}
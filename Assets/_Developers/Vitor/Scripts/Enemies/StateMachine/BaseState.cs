namespace Enemies.StateMachine
{
    public class BaseState
    {
        public EnemyStateMachine owner;
        public virtual void PrepareState(){}
        public virtual void UpdateState(){}
        public virtual void DestroyState(){}
    }
}
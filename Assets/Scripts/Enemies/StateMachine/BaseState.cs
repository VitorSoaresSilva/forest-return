namespace Enemies.StateMachine
{
    public class BaseState
    {
        public StateMachine owner;
        public virtual void PrepareState(){}
        public virtual void UpdateState(){}
        public virtual void DestroyState(){}
    }
}
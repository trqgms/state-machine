namespace StateMachine.Abilities
{
    public abstract class StateAbility
    {
        public virtual void OnCreated()
        {
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnDestroyed()
        {
        }
    }
}
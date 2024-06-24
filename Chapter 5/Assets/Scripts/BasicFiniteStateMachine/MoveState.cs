namespace BasicFiniteStateMachine
{
    public class MoveState : IState
    {
        private Actor _actor;

        public MoveState(Actor actor)
        {
            _actor = actor;
        }

        public void OnStateEnter()
        {
            _actor.SetIsWalking(true);
        }

        public void OnStateExit()
        {
            _actor.SetIsWalking(false);
        }

        public void OnStateUpdate()
        {
            _actor.MoveTowardsPlayer();
        }
    }
}
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

        }

        public void OnStateExit()
        {
            _actor.SetIsWalking(false);
        }

        public void OnStateUpdate()
        {
            if(_actor.IsAttacking())
            {
                return;
            }

            _actor.SetIsWalking(true);
            _actor.MoveTowardsPlayer();
        }
    }
}
namespace BasicFiniteStateMachine
{
    public class AttackState : IState
    {
        private Actor _actor;

        public AttackState(Actor actor)
        {
            _actor = actor;
        }

        public void OnStateEnter()
        {

        }

        public void OnStateExit()
        {

        }

        public void OnStateUpdate()
        {
            if(_actor.IsAttacking())
            {
                return;
            }

            _actor.LookTowardsPlayer();
            _actor.Attack();
        }
    }
}
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

            if(_actor.GetVectorBetweenPlayerAndActor().sqrMagnitude <= 3)
            {
                _actor.Attack();
            }
            else
            {
                _actor.SwitchState(StateEnum.None);
            }
        }
    }
}
namespace BasicFiniteStateMachine
{
    public class NoneState : IState
    {
        private Actor _actor;

        public NoneState(Actor actor)
        {
            _actor = actor;
        }

        public void OnStateEnter()
        {
            if(_actor.GetVectorBetweenPlayerAndActor().sqrMagnitude > 3)
            {
                _actor.SwitchState(StateEnum.Move);
            }
            else
            {
                _actor.SwitchState(StateEnum.Attack);
            }
        }

        public void OnStateExit()
        {
            
        }

        public void OnStateUpdate()
        {
            
        }
    }
}
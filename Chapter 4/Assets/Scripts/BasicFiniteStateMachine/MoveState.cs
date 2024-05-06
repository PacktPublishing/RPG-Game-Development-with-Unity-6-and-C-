using UnityEngine;

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
            if(_actor.GetVectorBetweenPlayerAndActor().sqrMagnitude > 3)
            {
                _actor.MoveTowardsPlayer();
            }
            else
            {
                _actor.SwitchState(StateEnum.None);
            }
        }
    }
}
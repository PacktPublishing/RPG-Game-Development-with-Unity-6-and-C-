using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class BlockState : IState
    {
        private Actor _actor;
        public BlockState(Actor actor)
        {
            _actor = actor;
        }

        public void OnStateEnter()
        {
            _actor.SetIsBlocking(Random.Range(0,2) > 0);
        }

        public void OnStateExit()
        {
            _actor.SetIsBlocking(false);
        }

        public void OnStateUpdate()
        {

        }
    }
}
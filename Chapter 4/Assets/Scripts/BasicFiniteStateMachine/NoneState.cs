using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class NoneState : IState
    {
        private Transform _playerTransform;
        private Actor _actor;

        public NoneState(Transform playerTransform, Actor actor)
        {
            _playerTransform = playerTransform;
            _actor = actor;
        }

        public void OnEntry()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void OnUpdate()
        {
            if((_playerTransform.position - _actor.transform.position).sqrMagnitude < 3)
            {
                _actor.SwitchToState(StateEnum.Attack);
            }
            else
            {
                _actor.SwitchToState(StateEnum.Move);
            }
        }
    }
}
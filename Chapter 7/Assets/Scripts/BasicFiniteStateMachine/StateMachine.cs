using System.Collections.Generic;
using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        private Dictionary<StateEnum, IState> _statesMap = new();
        private IState _currentState;
 
        public void SwitchState(StateEnum state)
        {
            if(_currentState == _statesMap[state])
            {
                return;
            }

            _currentState.OnStateExit();
            _currentState = _statesMap[state];
            _currentState.OnStateEnter();
        }

        private void Start()
        {
            _statesMap.Add(StateEnum.Attack, new AttackState(_actor));
            _statesMap.Add(StateEnum.Move, new MoveState(_actor));
            _statesMap.Add(StateEnum.None, new NoneState());
            _statesMap.Add(StateEnum.Block, new BlockState(_actor));
            _currentState = _statesMap[StateEnum.None];
            _currentState.OnStateEnter();
        }
 
        private void Update()
        {
            _currentState.OnStateUpdate();
        }
    }
}
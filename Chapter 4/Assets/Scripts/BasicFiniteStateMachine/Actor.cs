using System.Collections.Generic;
using Fight;
using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class Actor : MonoBehaviour, IReceiveDamage, IHaveHealth
    {
        public Dictionary<StateEnum, IState> _statesMap = new();
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _playerTransfrom;
        [SerializeField] private DamageTaker _damageTaker;
        private IState _activeState;

        public int Health {get; private set;}

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _damageTaker.RegisterDamageReceiver(this);
            Health = 100;
            _statesMap.Add(StateEnum.None, new NoneState(_playerTransfrom, this));
            _statesMap.Add(StateEnum.Attack, new AttackState(_animator, this, _playerTransfrom));
            _statesMap.Add(StateEnum.Move, new MoveState(this, _playerTransfrom, _characterController, _animator));
            _activeState = _statesMap[StateEnum.None];
        }

        public void SwitchToState(StateEnum stateEnum)
        {
            _activeState.OnExit();
            _activeState = _statesMap[stateEnum];
            _activeState.OnEntry();
        }

        // Update is called once per frame
        void Update()
        {
            _activeState.OnUpdate();
        }

        public void TakeDamage(int value)
        {
            Health -= value;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
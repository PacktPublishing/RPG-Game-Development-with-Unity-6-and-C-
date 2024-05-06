using System.Collections.Generic;
using Fight;
using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class Actor : MonoBehaviour, IReceiveDamage, IHaveHealth
    {
        [SerializeField] private DamageTaker[] damageTakers;
        [SerializeField] private Transform _playerTransfrom;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed = 1;
        [SerializeField] private Animator _animator;
        private Dictionary<StateEnum, IState> _statesMap = new();
        private IState _currentState;

        public int Health {get; private set;}

        public void ReceiveDamage(int damage)
        {
            Health -= damage;
            if(Health < 0)
            {
                Destroy(gameObject);
            }
        }

        public void MoveTowardsPlayer()
        {
            transform.rotation = Quaternion.LookRotation(GetVectorBetweenPlayerAndActor());
            _characterController.Move(transform.forward * _speed * Time.deltaTime);
        }

        public void SetIsWalking(bool value)
        {
            _animator.SetBool("IsWalking", value);
        }

        public void Attack()
        {
            _animator.SetTrigger("Slash");
        }

        public Vector3 GetVectorBetweenPlayerAndActor()
        {
            return _playerTransfrom.position - transform.position;
        }

        public void SwitchState(StateEnum state)
        {
            _currentState.OnStateExit();
            _currentState = _statesMap[state];
            _currentState.OnStateEnter();
        }

        public bool IsAttacking()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName("Slash");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _statesMap.Add(StateEnum.Attack, new AttackState(this));
            _statesMap.Add(StateEnum.Move, new MoveState(this));
            _statesMap.Add(StateEnum.None, new NoneState(this));
            _currentState = _statesMap[StateEnum.None];
            _currentState.OnStateEnter();

            Health = 100;
            foreach (var taker in damageTakers)
            {
                taker.RegisterDamageReceiver(this);
            }
        }

        void Update()
        {
            _currentState.OnStateUpdate();
        }
    }
}
